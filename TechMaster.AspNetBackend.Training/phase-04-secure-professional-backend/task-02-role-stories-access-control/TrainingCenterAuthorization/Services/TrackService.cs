using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
            return await context.TrainingTracks.Include(t => t.Instructor).Include(t => t.Enrollments).Where(t => t.TrainingTrackId== id && !t.IsDeleted).Select(t=>new TrackDetailsResponse
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
        public async Task<object> GetAvailableTracks()
        {
            return await context.TrainingTracks.Where(t => t.Status == TrackStatus.Active)
                .Select(t => new 
                {
                    t.TrainingTrackId,
                    t.Title,
                    t.Code,
                    t.Capacity,
                    t.Fee
                }).ToListAsync();
        }
        public async Task<object> GetInstructorTracks(string email)
        {
            return await context.TrainingTracks.Where(t => t.Instructor.Email == email)
                .Select(t => new
                {
                    t.TrainingTrackId,
                    t.Title,
                    t.Code,
                    t.StartDate,
                    t.EndDate,
                    t.Status

                }).ToListAsync();
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
                Fee = request.Fee,
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
        private async Task<object> GetStudentsForTrack(int trackId)
        {
            return await context.Enrollments
                .Where(e => e.TrainingTrackId == trackId)
                .Select(e => new
                {
                    e.StudentId,
                    e.Student.FullName,
                    e.Student.Email,
                    e.Status,
                    e.EnrollmentDate
                })
                .ToListAsync();
        }
        public async Task<object?> GetTrackStudents(int trackId,ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin"))
            {
                return await GetStudentsForTrack(trackId);
            }
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Invalid instructor token.");

            var instructor = await context.Instructors
                .FirstOrDefaultAsync(i => i.Email.ToLower() == email.ToLower());

            if (instructor == null)
                throw new UnauthorizedAccessException("Instructor is not linked to an instructor record.");
            var track = await context.TrainingTracks.Include(t => t.Enrollments).ThenInclude(e => e.Student).FirstOrDefaultAsync(t => t.TrainingTrackId == trackId);
            if (track == null) 
                return null;
             if (track.InstructorId != instructor.InstructorId)
                throw new UnauthorizedAccessException("You are not authorized to access this track.");

            return await GetStudentsForTrack(trackId);
            //    var role = user.FindFirst(ClaimTypes.Role)?.Value;
            //    if(role == "Admin")
            //       {
            //        return track.Enrollments.Where(e => e.Student != null).Select(e => new
            //        {
            //            StudentId = e.Student.StudentId,
            //            FullName = e.Student.FullName,
            //            Email = e.Student.Email,
            //            EnrollmentId = e.EnrollmentId,
            //            Status = e.Status,
            //            EnrollmentDate = e.EnrollmentDate
            //        }).ToList();
            //    }
            //    if(role == "Instructor")
            //    {
            //        var instructorId = user.FindFirst("InstructorId")?.Value;
            //        if (string.IsNullOrEmpty(instructorId))
            //            return null;
            //        if (track.InstructorId.ToString() != instructorId)
            //            throw new UnauthorizedAccessException("You can only view students in your own tracks ");
            //        return track.Enrollments.Where(e => e.Student != null).Select(e => new
            //        {
            //            StudentId = e.Student.StudentId,
            //            FullName = e.Student.FullName,
            //            Email = e.Student.Email,
            //            EnrollmentId = e.EnrollmentId,
            //            Status = e.Status,
            //            EnrollmentDate = e.EnrollmentDate
            //        }).ToList();
            //    }
            //    throw new UnauthorizedAccessException("Access denied");
        }
    }
}
