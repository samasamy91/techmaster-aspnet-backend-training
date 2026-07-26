using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.DTOs.Tracks;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;

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
            var query = context.TrainingTracks.Include(t => t.Instructor).Include(t => t.Enrollments).Where(t => !t.IsDeleted).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(t=>t.Title.Contains(keyword) || t.Code.Contains(keyword));
            }
            if (!string.IsNullOrWhiteSpace(level))
            {
                query = query.Where(t => t.Level.ToString() == level);
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(t => t.Status.ToString() == status);
            }
            if (instructorId.HasValue)
            {
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
            //Unique Code
            bool codeExists = await context.TrainingTracks.AnyAsync(t => t.Code == request.Code);
            if (codeExists)
            {
                throw new BadHttpRequestException("Track code already exists");
            }
            //Instructor Exists
            bool instructorExists = await context.Instructors.AnyAsync(i => i.InstructorId == request.InstructorId);
            if (!instructorExists)
            {
                throw new BadHttpRequestException("Instructor not found");
            }
            //Capacity > 0
            if(request.Capacity <= 0)
            {
                throw new BadHttpRequestException("Capacity must be greater than zero");
            }
            //Title Is Required
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new BadHttpRequestException("Track title is required");
            }
            //Start Before End
            if(request.StartDate >= request.EndDate)
            {
                throw new BadHttpRequestException("Start date must be before end date");
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
            ?? throw new BadHttpRequestException("Track created but could not be retrieved.");
        }
        public async Task<bool> UpdateTrack(int id,UpdateTrackRequest request)
        {
            var track = await context.TrainingTracks.FirstOrDefaultAsync(t=>t.TrainingTrackId == id && !t.IsDeleted);
            if (track == null)
                return false;
            var codeExists = await context.TrainingTracks.AnyAsync(t=>t.Code == request.Code &&
            t.TrainingTrackId != id );
            if (codeExists)
                throw new BadHttpRequestException("Track code already exists");
            bool instructorExists = await context.Instructors.AnyAsync(i => i.InstructorId == request.InstructorId);
            if (!instructorExists)
            {
                throw new BadHttpRequestException("Instructor not found");
            }
            if (request.Capacity <= 0)
            {
                throw new BadHttpRequestException("Capacity must be greater than zero");
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
                throw new BadHttpRequestException("Cannot delete a track with active enrollments ");
            track.IsDeleted = true;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
