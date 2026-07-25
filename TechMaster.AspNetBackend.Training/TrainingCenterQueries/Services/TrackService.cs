using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.DTOs.Tracks;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterQueries.DTOs.Tracks;

namespace TrainingCenter.Api.Services
{
    public class TrackService : ITrackService
    {
        private readonly AppDbContext context;
        public TrackService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<PaginationResult<TrackDetailsResponse>> GetAllTracks(string? keyword,
            string? level, string? status, int? instructorId, int pageNumber, int pageSize)
        {
            //Query 4  (Track Search) Return Only Active Queries

            var query = context.TrainingTracks.Include(t => t.Instructor).Include(t => t.Enrollments).Where(t => !t.IsDeleted && 
            t.Status == TrackStatus.Active).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(t=>t.Title.ToLower().Contains(keyword) || t.Code.ToLower().Contains(keyword) ||
                (!string.IsNullOrEmpty(t.Description) && t.Description.ToLower().Contains(keyword)));
            }
            //Query 5 Filter By Level
            if (!string.IsNullOrWhiteSpace(level))
            {
                if(!Enum.TryParse<TrackLevel>(level,true,out var trackLevel))
                {
                    throw new Exception("Invalid track leve, allowed values are beginner , intermediate , advanced");
                }
                query = query.Where(t => t.Level == trackLevel);
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (Enum.TryParse<TrackStatus>(status, true, out var trackStatus))
                {
                    query = query.Where(t => t.Status == trackStatus);
                }
            }
            //Query 6 Filter By Instructor Id
            if (instructorId.HasValue)
            {
                var instructorExists = await context.Instructors.AnyAsync(i => i.InstructorId == instructorId.Value);
                if (!instructorExists)
                    throw new Exception("Instructor not found");
                query = query.Where(t => t.InstructorId == instructorId.Value);
            }
            var totalRecord = await query.CountAsync();
            var tracks = await query.OrderBy(t=>t.StartDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(t => new TrackDetailsResponse
            {
                TrainingTrackId = t.TrainingTrackId,
                Title = t.Title,
                Code = t.Code,
                Level = t.Level,
                Status = t.Status,
                Capacity = t.Capacity,
                EnrolledStudents = t.Enrollments.Count,
                InstructorName = t.Instructor.FullName
            }).ToListAsync();
            return new PaginationResult<TrackDetailsResponse>
            {
                Items = tracks,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecord
            };
        }
        
        public async Task<TrackDetailsResponse?> GetTrackById(int id)
        {
            return await context.TrainingTracks.Include(t => t.Instructor).Include(t => t.Enrollments).Where(t => !t.IsDeleted).Select(t=>new TrackDetailsResponse
            {
                TrainingTrackId = t.TrainingTrackId,
                Title = t.Title,
                Code = t.Code,
                Level = t.Level,
                Status = t.Status,
                Capacity = t.Capacity,
                EnrolledStudents= t.Enrollments.Count,
                InstructorName= t.Instructor.FullName
            }).FirstOrDefaultAsync();
        }
        public async Task<TrackDetailsResponse> CreateTrack(CreateTrackRequest request)
        {
            bool codeExists = await context.TrainingTracks.AnyAsync(t => t.Code == request.Code);
            if (codeExists)
            {
                throw new Exception("Track code already exists");
            }
            bool instructorExists = await context.Instructors.AnyAsync(i => i.InstructorId == request.InstructorId);
            if (!instructorExists)
            {
                throw new Exception("Instructor not found");
            }
            if(request.Capacity <= 0)
            {
                throw new Exception("Capacity must be greater than zero");
            }
            var track = new TrainingTrack
            {
                Title = request.Title,
                Code = request.Code,
                Level = request.Level,
                Description = request.Description,
                Capacity = request.Capacity,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = Entities.Enums.TrackStatus.Upcoming,
                InstructorId = request.InstructorId,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            context.TrainingTracks.Add(track);
            await context.SaveChangesAsync();
            return await GetTrackById(track.TrainingTrackId)
            ?? throw new Exception("Track created but could not be retrieved.");
        }
        public async Task<bool> UpdateTrack(int id,UpdateTrackRequest request)
        {
            var track = await context.TrainingTracks.FirstOrDefaultAsync(t=>t.TrainingTrackId == id && !t.IsDeleted);
            if (track == null)
                return false;
            var codeExists = await context.TrainingTracks.AnyAsync(t=>t.Code == request.Code &&
            t.TrainingTrackId != id );
            if (codeExists)
                throw new Exception("Track code already exists");
            bool instructorExists = await context.Instructors.AnyAsync(i => i.InstructorId == request.InstructorId);
            if (!instructorExists)
            {
                throw new Exception("Instructor not found");
            }
            if (request.Capacity <= 0)
            {
                throw new Exception("Capacity must be greater than zero");
            }
            track.Title = request.Title;
            track.Code = request.Code;
            track.Description = request.Description;
            track.Level = request.Level;
            track.Capacity = request.Capacity;
            track.StartDate = request.StartDate;
            track.EndDate = request.EndDate;
            track.Status = request.Status;
            track.InstructorId = request.InstructorId;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTrack(int id)
        {
            var track = await context.TrainingTracks.Include(t => t.Enrollments).FirstOrDefaultAsync(
                t=>t.TrainingTrackId == id && !t.IsDeleted);
            if (track == null) return false;
            bool hasActiveEnrollments = track.Enrollments.Any(e => e.Status == EnrollmentStatus.Active);

            if (hasActiveEnrollments)
                throw new Exception("Cannot delete a track with active enrollments ");
            track.IsDeleted = true;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
