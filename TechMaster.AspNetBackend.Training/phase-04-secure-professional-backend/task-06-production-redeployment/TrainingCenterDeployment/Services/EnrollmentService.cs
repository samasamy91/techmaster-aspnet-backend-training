using Microsoft.EntityFrameworkCore;
using SecurePlatformUpgrade.DTOs.Enrollments;
using System.Security.Claims;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Enrollments;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;
using ValidationTrainingCenter.Common.Exceptions;

namespace TrainingCenter.Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext context;
        private readonly ILogger<EnrollmentService> logger;
        public EnrollmentService(AppDbContext context, ILogger<EnrollmentService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<IEnumerable<EnrollmentDetailsResponse>> GetAllEnrollments(string? status,
            int? trackId, int? studentId)
        {
            var query = context.Enrollments.Include(e => e.Student).Include(e => e.TrainingTrack).Include(e => e.Payments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
            {
                if(Enum.TryParse<EnrollmentStatus>(status,true,out var enrollmentStatus))
                {
                    query = query.Where(e => e.Status == enrollmentStatus);
                }
            }
            if (trackId.HasValue)
            {
                query = query.Where(e => e.TrainingTrackId == trackId);
            }
            if (studentId.HasValue)
            {
                query = query.Where(e => e.StudentId == studentId);
            }
            return await query.Select(e => new EnrollmentDetailsResponse
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FullName,
                TrackTitle = e.TrainingTrack.Title,
                Status = e.Status,
                ProgressPercentage = e.ProgressPercentage,
                FinalResult = e.FinalResult
            }).ToListAsync();
        }
        public async Task<EnrollmentDetailsResponse?> GetEnrollmentById(int id)
        {
            return await context.Enrollments.Include(e => e.Student).Include(e => e.TrainingTrack).Where(e => e.EnrollmentId == id)
                .Select(e => new EnrollmentDetailsResponse
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FullName,
                Status = e.Status,
                ProgressPercentage = e.ProgressPercentage,
                FinalResult = e.FinalResult,
                TrackTitle = e.TrainingTrack.Title,
            }).FirstOrDefaultAsync();
        }
        public async Task<object?> GetStudentEnrollment(string email)
        {
            return await context.Enrollments.Where(e => e.Student.Email == email)
                .Select(e => new EnrollmentDetailsResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentName = e.Student.FullName,
                    Status = e.Status,
                    ProgressPercentage = e.ProgressPercentage,
                    FinalResult = e.FinalResult,
                    TrackTitle = e.TrainingTrack.Title,
                }).ToListAsync();
        }
        public async Task<object> GetInstructorEnrollments(string email)
        {
            return await context.Enrollments.Where(e=>e.TrainingTrack.Instructor.Email == email)
                .Select(e=> new EnrollmentDetailsResponse
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FullName,
                Status = e.Status,
                ProgressPercentage = e.ProgressPercentage,
                FinalResult = e.FinalResult,
                TrackTitle = e.TrainingTrack.Title,
            }).ToListAsync();
        }
        public async Task<EnrollmentDetailsResponse> CreateEnrollment(CreateEnrollmentRequest request)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.StudentId == request.StudentId && !s.IsDeleted);

            if (student == null)
                throw new NotFoundException("Student not found.");

            if (student.IsDeleted)
                throw new BusinessRuleException("Student deleted.");

            if (!student.IsActive)
                throw new BusinessRuleException("Student not active.");


            var track = await context.TrainingTracks.Include(t => t.Enrollments).FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == request.TrainingTrackId && !t.IsDeleted);

            if (track == null)
                throw new NotFoundException("Training track not found.");
            if (track.Status == TrackStatus.Cancelled)
                throw new BusinessRuleException("Cannot enroll in a cancelled track");
            if (track.Status == TrackStatus.Completed)
                throw new BusinessRuleException("Cannot enroll in a completed track");

            bool alreadyEnrolled = await context.Enrollments.AnyAsync(e =>
                    e.StudentId == request.StudentId && e.TrainingTrackId == request.TrainingTrackId && 
                    e.Status == EnrollmentStatus.Active);

            if (alreadyEnrolled)
                throw new BusinessRuleException("Student already has active enrollment in this track.");

            if (track.Enrollments.Count(e=>e.Status == EnrollmentStatus.Active) >= track.Capacity)
                throw new BusinessRuleException("Track capacity has been reached.");
          
            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                TrainingTrackId = request.TrainingTrackId,
                EnrollmentDate = DateTime.UtcNow,
                Status = EnrollmentStatus.Pending,
                ProgressPercentage = 0,
                CreatedAt = DateTime.UtcNow
            };
            
            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

            logger.LogInformation("Enrollment created, EnrollmentId: {EnrollmentId}, StudentId: {StudentId}, TrackId: {TrackId}",
                enrollment.EnrollmentId, enrollment.StudentId, enrollment.TrainingTrackId);
            return await GetEnrollmentById(enrollment.EnrollmentId)
                ?? throw new BusinessRuleException("Enrollment created but could not be retrieved.");
        }
        public async Task<bool> UpdateStatusEnrollment(int id,UpdateEnrollmentStatusRequest request)
        {
            var enrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null)
                throw new NotFoundException("Enrollment not found");

            bool validTransition =enrollment.Status switch
                {
                    EnrollmentStatus.Pending =>
                        request.Status == EnrollmentStatus.Active ||
                        request.Status == EnrollmentStatus.Cancelled,

                    EnrollmentStatus.Active =>
                        request.Status == EnrollmentStatus.Completed ||
                        request.Status == EnrollmentStatus.Suspended ||
                        request.Status == EnrollmentStatus.Cancelled,

                    EnrollmentStatus.Suspended =>
                        request.Status == EnrollmentStatus.Active ||
                        request.Status == EnrollmentStatus.Cancelled,

                    EnrollmentStatus.Completed =>
                        request.Status == EnrollmentStatus.Active ||
                        request.Status == EnrollmentStatus.Cancelled,
                    _ => false
                };

            if (!validTransition)
                throw new BusinessRuleException("Invalid enrollment status transition.");
            
            enrollment.Status = request.Status;
            enrollment.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            logger.LogInformation("Enrollment status changed, EnrollmentId: {EnrollmentId}, NewStatus: {Status}",
                enrollment.EnrollmentId,enrollment.Status);
            return true;
        }
        public async Task<IEnumerable<EnrollmentDetailsResponse>> GetStudentEnrollments(int studentId)
        {
            return await context.Enrollments.Include(e => e.Student).Include(e => e.TrainingTrack).Where(e => e.StudentId == studentId)
                .Select(e => new EnrollmentDetailsResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentName = e.Student.FullName,
                    TrackTitle = e.TrainingTrack.Title,
                    Status = e.Status,
                    ProgressPercentage = e.ProgressPercentage,
                    FinalResult = e.FinalResult
                }).ToListAsync();
        }
        public async Task<IEnumerable<EnrollmentDetailsResponse>> GetTrackStudents(int trackId)
        {
            return await context.Enrollments.Include(e => e.Student).Include(e => e.TrainingTrack).Where(e => e.TrainingTrackId == trackId)
                .Select(e => new EnrollmentDetailsResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentName = e.Student.FullName,
                    TrackTitle = e.TrainingTrack.Title,
                    Status = e.Status,
                    ProgressPercentage = e.ProgressPercentage,
                    FinalResult = e.FinalResult
                }).ToListAsync();
        }
        public async Task<object> RequestEnrollment(StudentEnrollmentRequest request,ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrWhiteSpace(email))
                throw new UnauthorizedAccessException("Student identity not found");

            var student = await context.Students.FirstOrDefaultAsync(s => s.Email == email);
            if (student == null)
                throw new NotFoundException("Student account not found");

            var track = await context.TrainingTracks.FirstOrDefaultAsync(t => t.TrainingTrackId == request.TrainingTrackId);
            if (track == null)
                throw new NotFoundException("Training track not found");

            if (track.Status != TrackStatus.Active)
                throw new BusinessRuleException("Track not acception enrollments");

            var existingEnroll = await context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.TrainingTrackId == track.TrainingTrackId && e.Status == EnrollmentStatus.Active);
            if (existingEnroll)
                throw new BusinessRuleException("You already enrolled in this track");

            var activeCount = await context.Enrollments.CountAsync(e => e.TrainingTrackId == request.TrainingTrackId && e.Status == EnrollmentStatus.Active);

            if (activeCount >= track.Capacity)
                throw new BusinessRuleException("This track is full");

            var enrollment = new Enrollment
            {
                StudentId = student.StudentId,
                TrainingTrackId = track.TrainingTrackId,
                EnrollmentDate = DateTime.UtcNow,
                Status = EnrollmentStatus.Pending,
                ProgressPercentage = 0
            };
            await context.Enrollments.AddAsync(enrollment);
            await context.SaveChangesAsync();
            return new
            {
                enrollment.EnrollmentId,
                enrollment.StudentId,
                enrollment.TrainingTrackId,
                enrollment.Status,
                enrollment.EnrollmentDate
            };
        }

    }
}
