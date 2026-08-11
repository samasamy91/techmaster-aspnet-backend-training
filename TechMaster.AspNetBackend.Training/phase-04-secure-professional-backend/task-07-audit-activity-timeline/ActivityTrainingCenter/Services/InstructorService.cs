using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SecurePlatformUpgrade.DTOs.Instructors;
using SecurePlatformUpgrade.DTOs.TrackSession;
using SecurePlatformUpgrade.Entities;
using System.Security.Claims;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Instructors;
using TrainingCenter.Api.DTOs.Tracks;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Entities.Enums;
using TrainingCenterAuthTask01.Security;
using ValidationTrainingCenter.Common.Exceptions;

namespace TrainingCenter.Api.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext context;
        private readonly PasswordHasher passwordHasher;
        public InstructorService(AppDbContext context, PasswordHasher passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }
        public async Task<IEnumerable<InstructorResponse>> GetAllInstructor()
        {
            return await context.Instructors.OrderBy(i => i.FullName).Select(i => new InstructorResponse
            {
                InstructorId = i.InstructorId,
                FullName = i.FullName,
                Email = i.Email,
                Specialization = i.Specialization,
                IsActive = i.IsActive,
            }).ToListAsync();
        }
        public async Task<InstructorResponse?> GetInstructorById(int id)
        {
            return await context.Instructors.Where(i=>i.InstructorId == id).Select(i=>new InstructorResponse
            {
                InstructorId = i.InstructorId,
                FullName = i.FullName,
                Email = i.Email,
                Specialization = i.Specialization,
                IsActive = i.IsActive,
            }).FirstOrDefaultAsync();
        }
        public async Task<InstructorResponse> CreateInstructor(CreateInstructorRequest request)
        {
            bool emailExists = await context.Instructors.AnyAsync(i => i.Email == request.Email);
            if (emailExists)
                throw new BusinessRuleException("Instructor email already exists ");
            var instructor = new Instructor
            {
                FullName = request.FullName,
                Email = request.Email,
                Specialization = request.Specialization,
                Bio = request.Bio,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };
            context.Instructors.Add(instructor);
            await context.SaveChangesAsync();
            var user = new User
            {
                FullName = instructor.FullName,
                Email = instructor.Email,
                HashPassword = passwordHasher.Hash(request.Password),
                Role = UserRole.Instructor,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                InstructorId = instructor.InstructorId
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new InstructorResponse
            {
                InstructorId = instructor.InstructorId,
                FullName = instructor.FullName,
                Email = instructor.Email,
                Specialization = instructor.Specialization,
                IsActive = instructor.IsActive
            };
        }
        public async Task<bool> UpdateInstructor(int id,UpdateInstructorRequest request)
        {
            var instructor = await context.Instructors.FirstOrDefaultAsync(i => i.InstructorId == id);
            if (instructor == null)
                throw new NotFoundException("Instructor not found");
            bool emailExists = await context.Instructors.AnyAsync(i => i.Email == request.Email && i.InstructorId != id);
            if (emailExists)
                throw new BusinessRuleException("Instructor email is already exists");
            instructor.FullName = request.FullName;
            instructor.Email = request.Email;
            instructor.Specialization = request.Specialization;
            instructor.IsActive = request.IsActive;
            instructor.Bio = request.Bio;

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<TrackDetailsResponse>> GetTracksByInstructor(int instructorId)
        {
           return await context.TrainingTracks.Where(t=>t.InstructorId == instructorId && !t.IsDeleted).
                Select(t=>new TrackDetailsResponse
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
        }
        //Track Session
        public async Task<TrackSessionResponse> CreateTrackSession(int trackId,CreateTrackSessionRequest request,ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Instructor identity not found");
            var instructor = await context.Instructors.FirstOrDefaultAsync(i => i.Email == email);
            if (instructor == null)
                throw new NotFoundException("No instructor account is linked to this user");
            var track = await context.TrainingTracks.FirstOrDefaultAsync(t => t.TrainingTrackId == trackId && t.InstructorId == instructor.InstructorId);
            if (track == null)
                throw new NotFoundException("You are not assigned to this track");
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new BusinessRuleException("Session title is required");
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new BusinessRuleException("Session title is required.");
            if (request.SessionDate < track.StartDate || request.SessionDate > track.EndDate)
                throw new BusinessRuleException("Session date must be within the track dates.");
            if (track.InstructorId != instructor.InstructorId)
                throw new ForbiddenException("You are not authorized to manage this track.");

            var session = new TrackSession
            {
                TrainingTrackId = trackId,
                SessionDate = request.SessionDate,
                Title = request.Title,
                Description = request.Description,
                MeetionLink = request.MeetingLink,
                IsCompleted = false,
                CreatedByInstructorId = instructor.InstructorId,
            };
            context.TrackSessions.Add(session);
            await context.SaveChangesAsync();
            
            return new TrackSessionResponse
            {
                TrackSessionId = session.TrackSessionId,
                TrainingTrackId = session.TrainingTrackId,
                SessionDate = session.SessionDate,
                Title = session.Title,
                Description = session.Description,
                MeetionLink = session.MeetionLink,
                IsCompleted = session.IsCompleted,
                CreatedByInstructorId = session.CreatedByInstructorId
            };
        }
        public async Task<TrackSessionResponse?> UpdateTrackSession(int sessionId, UpdateTrackSessionRequest request, ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Instructor identity not found");
            
            var instructor = await context.Instructors.FirstOrDefaultAsync(i => i.Email == email);
            if (instructor == null)
                throw new NotFoundException("Instructor account not found");
            
            var session = await context.TrackSessions.Include(s=>s.TrainingTrack).FirstOrDefaultAsync(s=>s.TrainingTrackId == sessionId);
            if (session == null)
                return null;
            if (session.TrainingTrack == null)
                throw new NotFoundException("Training track not found for this session");
            if (session.TrainingTrack.InstructorId != instructor.InstructorId)
                throw new ForbiddenException("You are not assigned to this track");
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new BusinessRuleException("Session Title is required");

            session.SessionDate = request.SessionDate;
            session.Title = request.Title;
            session.Description = request.Description;
            session.MeetionLink = request.MeetingLink;
            session.IsCompleted = request.IsCompleted;

            await context.SaveChangesAsync();

            return new TrackSessionResponse
            {
                TrackSessionId = session.TrackSessionId,
                TrainingTrackId = session.TrainingTrackId,
                SessionDate = session.SessionDate,
                Title = session.Title,
                Description = session.Description,
                MeetionLink = session.MeetionLink,
                IsCompleted = session.IsCompleted,
                CreatedByInstructorId = session.CreatedByInstructorId,
            };
            
        }
        //Track Progress
        public async Task<TrackProgressResponse?> GetTrackProgress(int trackId,ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("Instructor identity not found");

            var instructor = await context.Instructors.FirstOrDefaultAsync(i => i.Email == email);
            if (instructor == null)
                throw new NotFoundException("Instructor account not found");

            var track = await context.TrainingTracks.FirstOrDefaultAsync(t => t.TrainingTrackId == trackId && t.InstructorId == instructor.InstructorId);
            if (track == null)
                return null;

            if (track.InstructorId != instructor.InstructorId)
                throw new ForbiddenException("You are not assigned to this track");

            var enrollments = await context.Enrollments.Where(e => e.Status != EnrollmentStatus.Cancelled).ToListAsync();
            var totalSudents = enrollments.Count;

            var avgStudents = totalSudents == 0 ? 0 : enrollments.Average(e => e.ProgressPercentage);

            var completedStudents = enrollments.Count(e => e.ProgressPercentage >= 100);
            var activeStudents = enrollments.Count(e => e.Status == EnrollmentStatus.Active);

            return new TrackProgressResponse
            {
                TrainingTrackId = track.TrainingTrackId,
                TrackTitle = track.Title,
                TotalStudents = totalSudents,
                CompletedStudents = completedStudents,
                ActiveStudents = activeStudents,
                AvgProgress = avgStudents,
            };
            
        }
    }
}
