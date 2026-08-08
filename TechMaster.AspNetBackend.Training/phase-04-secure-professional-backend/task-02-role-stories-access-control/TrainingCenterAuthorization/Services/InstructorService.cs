using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Instructors;
using TrainingCenter.Api.DTOs.Tracks;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Entities.Enums;
using TrainingCenterAuthTask01.Security;

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
                throw new Exception("Instructor email already exists ");
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
                return false;
            bool emailExists = await context.Instructors.AnyAsync(i => i.Email == request.Email && i.InstructorId != id);
            if (emailExists)
                throw new Exception("Instructor email is already exists");
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
    }
}
