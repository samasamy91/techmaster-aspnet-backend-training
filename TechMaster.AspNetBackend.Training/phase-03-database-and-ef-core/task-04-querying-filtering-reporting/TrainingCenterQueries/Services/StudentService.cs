using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext context;
        public StudentService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<PaginationResult<StudentListItemResponse>> GetAllStudent(string? search,
            bool? isActive,int pageNumber,int pageSize)
        {
            var query = context.Students.Where(s => !s.IsDeleted).AsQueryable();
            
            //Query 1 (Search Student)

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(s => s.FullName.ToLower().Contains(search) || s.Email.ToLower().Contains(search) || 
                (s.PhoneNumber != null && s.PhoneNumber.ToLower().Contains(search)));
            }

            //Query 2 (Filter By Status)

            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }

            //Query 3 Paged Student List

            if (pageNumber < 1)
                throw new Exception("Page number must be greater than zero");
            if (pageSize < 1 || pageSize > 100)
                throw new Exception("Page size must be between 1 and 100");
            var totalRecord = await query.CountAsync();
            var students = await query.OrderBy(s=>s.FullName).Skip((pageNumber-1)*pageSize).Take(pageSize).Select(s=>new StudentListItemResponse
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                Email = s.Email,
                IsActive = s.IsActive,
                EnrollmentCount = s.Enrollments.Count
            }).ToListAsync();
            return new PaginationResult<StudentListItemResponse>
            {
                Items = students,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecord
            };
        }
        public async Task<StudentDetailsResponse?> GetStudentById(int id)
        {
            return await context.Students.Where(s => s.StudentId == id && !s.IsDeleted).Select(s => new StudentDetailsResponse
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                Email = s.Email,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                PhoneNumber = s.PhoneNumber,
                EnrollmentCount = s.Enrollments.Count
            }).FirstOrDefaultAsync();
        }
        public async Task<StudentDetailsResponse> CreateStudent(CreateStudentRequest request)
        {
            var emailExists = await context.Students.AnyAsync(s => s.Email == request.Email && !s.IsDeleted);
            if (emailExists)
            {
                throw new Exception("Email already exists");
            }
            var student = new Student
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            context.Students.Add(student);
            await context.SaveChangesAsync();
            return new StudentDetailsResponse
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                IsActive = student.IsActive,
                CreatedAt = student.CreatedAt,
                EnrollmentCount = 0
            };
        }
        public async Task<bool> UpdateStudent(int id,UpdateStudentRequest request)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.StudentId == id && !s.IsDeleted);
            if (student == null)
                return false;
            var emailExists = await context.Students.AnyAsync(s=>s.Email == request.Email && 
            s.StudentId != id && !s.IsDeleted);
            if (emailExists)
                throw new Exception("Email already exists");
            student.FullName = request.FullName;
            student.Email = request.Email;
            student.PhoneNumber = request.PhoneNumber;
            student.IsActive = request.IsActive;
            student.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteStudent(int id)
        {
            var student = await context.Students.FirstOrDefaultAsync(
                s=>s.StudentId == id && !s.IsDeleted);
            if (student == null) return false;
            student.IsDeleted = true;
            student.IsActive = false;
            student.DeletedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }
    }
}