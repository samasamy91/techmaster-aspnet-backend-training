using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Enrollments;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext context;
        public EnrollmentService(AppDbContext context)
        {
            this.context = context;
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
            return await context.Enrollments.Include(e => e.Student).Include(e => e.TrainingTrack).Where(e => e.EnrollmentId == id).Select(e => new EnrollmentDetailsResponse
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FullName,
                Status = e.Status,
                ProgressPercentage = e.ProgressPercentage,
                FinalResult = e.FinalResult,
                TrackTitle = e.TrainingTrack.Title,
            }).FirstOrDefaultAsync();
        }
        public async Task<EnrollmentDetailsResponse> CreateEnrollment(CreateEnrollmentRequest request)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.StudentId == request.StudentId && !s.IsDeleted);
            //Inactive or deleted student cannot enroll rule
            if (!student.IsActive || student.IsDeleted)
                throw new BadHttpRequestException("Inactive or deleted students cannot enroll");
            if (student == null)
                throw new BadHttpRequestException("Student not found.");
            
            var track = await context.TrainingTracks.Include(t => t.Enrollments).FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == request.TrainingTrackId && !t.IsDeleted);

            if (track == null)
                throw new BadHttpRequestException("Training track not found.");
            //Capacity Rule
            var activeStudents = await context.Enrollments.CountAsync(e => e.TrainingTrackId == track.TrainingTrackId && e.Status == EnrollmentStatus.Active);
            if(activeStudents >= track.Capacity)
            {
                throw new BadHttpRequestException("Track capacity has been reached");
            }
            //Closed Track
            if(track.Status == TrackStatus.Completed)
            {
                throw new BadHttpRequestException("Closed tracks cannot accept enrollments");
            }
            //Duplicate Active Enrollment
            bool alreadyEnrolled = await context.Enrollments.AnyAsync(e =>
                    e.StudentId == request.StudentId && e.TrainingTrackId == request.TrainingTrackId && e.Status == EnrollmentStatus.Active);

            if (alreadyEnrolled)
                throw new BadHttpRequestException("Student is already active enrolled in this track.");

            if (track.Enrollments.Count >= track.Capacity)
                throw new BadHttpRequestException("Track capacity has been reached.");

            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                TrainingTrackId = request.TrainingTrackId,
                EnrollmentDate = DateTime.UtcNow,
                Status = EnrollmentStatus.Pending, //Default Status
                ProgressPercentage = 0,
                CreatedAt = DateTime.UtcNow
            };

            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

            return await GetEnrollmentById(enrollment.EnrollmentId)
                ?? throw new BadHttpRequestException("Enrollment created but could not be retrieved.");
        }
        public async Task<bool> UpdateStatusEnrollment(int id,UpdateEnrollmentStatusRequest request)
        {
            var enrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null)
                return false;
            //Completed cannot become cancelled
            if(enrollment.Status == EnrollmentStatus.Completed && request.Status == EnrollmentStatus.Cancelled)
            {
                throw new BadHttpRequestException("Completed enrollment canonot be cancelled");
            }
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

                    _ => false
                };

            if (!validTransition)
                throw new BadHttpRequestException("Invalid enrollment status transition.");

            enrollment.Status = request.Status;
            enrollment.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
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
        
    }
}
