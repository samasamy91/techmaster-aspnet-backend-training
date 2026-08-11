using ActivityTrainingCenter.DTOs.ActivityLogs;
using ActivityTrainingCenter.Entities;
using ActivityTrainingCenter.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Data;
using ValidationTrainingCenter.Common.Exceptions;

namespace ActivityTrainingCenter.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly AppDbContext context;
        public ActivityLogService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task Log(ClaimsPrincipal? user , string action, string entityName,
            int? entityId = null, string? desciption = null, string? ipAddress = null, string? metadata = null)
        {
            int? userId = null;
            string? userRole = null;

            if (user != null)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out var parsedUserId))
                {
                    userId = parsedUserId;
                }
                userRole = user.FindFirst(ClaimTypes.Role)?.Value ?? user.FindFirst("role")?.Value;
            }
            

            var activityLog = new ActivityLog
            {
                UserId = userId,
                UserRole = userRole,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                Description = desciption,
                CreatedAt = DateTime.UtcNow,
                IpAddress = ipAddress,
                Metadata = metadata
            };
            context.ActivityLogs.Add(activityLog);
            await context.SaveChangesAsync();
        }
        public async Task<PagedResult<ActivityLogDTO>> GetLogs(ActivityLogQueryDTO query)
        {
            var logs = context.ActivityLogs.AsNoTracking().AsQueryable();
            if (query.UserId.HasValue)
            {
                logs = logs.Where(x => x.UserId == query.UserId);
            }
            if (!string.IsNullOrWhiteSpace(query.EntityName))
                logs = logs.Where(x => x.EntityName == query.EntityName);

            if (query.From.HasValue)
                logs = logs.Where(x => x.CreatedAt >= query.From.Value);

            if (query.To.HasValue)
                logs = logs.Where(x=>x.CreatedAt <= query.To.Value);

            if (query.From.HasValue && query.To.HasValue && query.To.Value > query.From.Value)
                throw new BusinessRuleException("From date should be before to");

            if (query.PageNumber < 1)
                throw new BusinessRuleException("Page number must be greater than 0");

            var totaCount = await logs.CountAsync();

            var items = await logs.OrderByDescending(x => x.CreatedAt).Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize)
                .Select(x => new ActivityLogDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserRole = x.UserRole,
                    Action = x.Action,
                    EntityName = x.EntityName,
                    EntityId = x.EntityId,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    IpAddress = x.IpAddress,
                    Metadata = x.Metadata
                }).ToListAsync();
            return new PagedResult<ActivityLogDTO>
            {
                Items = items,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = totaCount
            }; 
        }
        
    }
}
