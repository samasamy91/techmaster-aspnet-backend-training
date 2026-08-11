using ActivityTrainingCenter.DTOs.ActivityLogs;
using System.Security.Claims;
using TrainingCenter.Api.Common;

namespace ActivityTrainingCenter.Services.IServices
{
    public interface IActivityLogService
    {
        Task Log(ClaimsPrincipal user, string action, string entityName,
            int? entityId = null, string? desciption = null, string? ipAddress = null, string? metadata = null);
        Task<PagedResult<ActivityLogDTO>> GetLogs(ActivityLogQueryDTO query);
    }
}
