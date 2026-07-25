using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Enrollments;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterQueries.DTOs.Enrollments;

namespace TrainingCenter.Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext context;
        public EnrollmentService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<EnrollmentListItemResponse>> GetAllEnrollments(string? status, int? trackId, int? studentId)
        {
            //Query 8 Enrollment List With Details
            var query = context.Enrollments.Include(e => e.Student).Include(e => e.TrainingTrack).AsQueryable();
            
            //Query 9 Filter By Status

            if (!string.IsNullOrWhiteSpace(status))
            {
                if(!Enum.TryParse<EnrollmentStatus>(status,true,out var enrollmentStatus))
                {
                    throw new Exception("Invalid enrollment status. Allowed values are Pending, Active, Completed, Cancelled.");
                }
                query = query.Where(e => e.Status == enrollmentStatus);
            }
            if (trackId.HasValue)
            {
                query = query.Where(e => e.TrainingTrackId == trackId);
            }
            if (studentId.HasValue)
            {
                query = query.Where(e => e.StudentId == studentId);
            }
            return await query.OrderByDescending(e=>e.EnrollmentDate).Select(e => new EnrollmentListItemResponse
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FullName,
                TrackTitle = e.TrainingTrack.Title,
                Status = e.Status,
                EnrollmentDate = e.EnrollmentDate,
                ProgressPercentage = e.ProgressPercentage,
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

            if (student == null)
                throw new Exception("Student not found.");

            var track = await context.TrainingTracks.Include(t => t.Enrollments).FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == request.TrainingTrackId && !t.IsDeleted);

            if (track == null)
                throw new Exception("Training track not found.");

            bool alreadyEnrolled = await context.Enrollments.AnyAsync(e =>
                    e.StudentId == request.StudentId && e.TrainingTrackId == request.TrainingTrackId);

            if (alreadyEnrolled)
                throw new Exception("Student is already enrolled in this track.");

            if (track.Enrollments.Count >= track.Capacity)
                throw new Exception("Track capacity has been reached.");

            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                TrainingTrackId = request.TrainingTrackId,
                EnrollmentDate = DateTime.UtcNow,
                Status = EnrollmentStatus.Active,
                ProgressPercentage = 0,
                CreatedAt = DateTime.UtcNow
            };

            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

            return await GetEnrollmentById(enrollment.EnrollmentId)
                ?? throw new Exception("Enrollment created but could not be retrieved.");
        }
        public async Task<bool> UpdateStatusEnrollment(int id,UpdateEnrollmentStatusRequest request)
        {
            var enrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null)
                return false;

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
                throw new Exception("Invalid enrollment status transition.");

            enrollment.Status = request.Status;
            enrollment.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }
        //Query 10 Student Enrollment History
        public async Task<IEnumerable<StudentEnrollmentHistoryResponse>> GetStudentEnrollments(int studentId)
        {
            return await context.Enrollments.Where(e => e.StudentId == studentId)
                .Select(e => new StudentEnrollmentHistoryResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    TrainingTrackId = e.TrainingTrackId,
                    TrackTitle = e.TrainingTrack.Title,
                    Status = e.Status,
                    EnrollmentDate = e.EnrollmentDate,
                    ProgressPercentage = e.ProgressPercentage,
                    FinalResult = e.FinalResult
                }).ToListAsync();
        }
        public async Task<IEnumerable<TrackStudentResponse>> GetTrackStudents(int trackId)
        {
            var trackExists =  await context.TrainingTracks.AnyAsync(t => t.TrainingTrackId == trackId && !t.IsDeleted);
            if (!trackExists)
                throw new Exception("Training track not found");
            return await context.Enrollments.Where(e=>e.TrainingTrackId == trackId).OrderBy(e=>e.Student.FullName)
            .Select(e => new TrackStudentResponse
                {
                    StudentId = e.StudentId,
                    FullName = e.Student.FullName,
                    Email = e.Student.Email,
                    Status = e.Status,
                    EnrollmentDate = e.EnrollmentDate
                }).ToListAsync();
        }
        
    }
}
