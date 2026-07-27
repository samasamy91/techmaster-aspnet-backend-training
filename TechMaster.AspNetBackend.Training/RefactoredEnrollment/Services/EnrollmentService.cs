using Microsoft.EntityFrameworkCore;
using RefactoredEnrollment.DTOs.Enrollments;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Data;
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
        public async Task<PaginationResult<EnrollmentList>> GetAll(int page,int pageSize)
        {
            var query = context.Enrollments.Where(e => !e.IsDeleted).Select(e => new EnrollmentList
            {
                Id = e.EnrollmentId,
                StudentName = e.Student.FullName,
                TrackName = e.TrainingTrack.Title,
                Status = e.Status
            });
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationResult<EnrollmentList>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
        public async Task<EnrollmentResponse> Create(CreateEnrollmentRequest request)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.StudentId == request.StudentId && !s.IsDeleted);
            if (student == null)
                throw new KeyNotFoundException("Student not found");
            var track = await context.TrainingTracks.Include(t => t.Enrollments).FirstOrDefaultAsync(t => t.TrainingTrackId == request.TrainingTrackId);
            if (track == null)
                throw new KeyNotFoundException("Training track not found");

            bool exists = await context.Enrollments.AnyAsync(e => e.StudentId == request.StudentId && e.TrainingTrackId == request.TrainingTrackId &&
            e.Status == EnrollmentStatus.Active);
            if (exists)
                throw new BadHttpRequestException("Student already has active enrollment in this track");
            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                TrainingTrackId = request.TrainingTrackId,
                EnrollmentDate = DateTime.UtcNow,
                Status = EnrollmentStatus.Active,
                IsDeleted = false
            };
            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();
            return new EnrollmentResponse
            {
                Id = request.StudentId,
                StudentName = student.FullName,
                TrackName = track.Title,
                EnrollmentDate = enrollment.EnrollmentDate,
                Status = enrollment.Status,
            };
        }
        
        
    }
}
