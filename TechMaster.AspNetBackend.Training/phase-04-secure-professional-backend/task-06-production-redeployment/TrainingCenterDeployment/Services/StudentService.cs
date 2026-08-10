using Microsoft.EntityFrameworkCore;
using SecurePlatformUpgrade.DTOs.Students;
using System.Security.Claims;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Entities.Enums;
using TrainingCenterAuthTask01.Security;
using ValidationTrainingCenter.Common.Exceptions;

namespace TrainingCenter.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext context;
        private readonly PasswordHasher passwordHasher;
        public StudentService(AppDbContext context,PasswordHasher passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }
        public async Task<PagedResult<StudentListItemResponse>> GetAllStudent(string? search,
            bool? isActive,int pageNumber,int pageSize)
        {
            var query = context.Students.Where(s => !s.IsDeleted).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.FullName.Contains(search) || s.Email.Contains(search));
            }
            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }
            var totalRecord = await query.CountAsync();
            var students = await query.OrderBy(s=>s.FullName).Skip((pageNumber-1)*pageSize).Take(pageSize).Select(s=>new StudentListItemResponse
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                Email = s.Email,
                IsActive = s.IsActive,
                EnrollmentCount = s.Enrollments.Count
            }).ToListAsync();
            return new PagedResult<StudentListItemResponse>
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

        public async Task<StudentDetailsResponse?> GetCurrentStudent(string email)
        {
           
            return await context.Students.Where(s => s.Email == email && !s.IsDeleted).Select(s => new StudentDetailsResponse
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
                throw new BusinessRuleException("Email already exists");
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
            var user = new User
            {
                FullName = student.FullName,
                Email = student.Email,
                HashPassword = passwordHasher.Hash(request.Password),
                Role = UserRole.Student,
                IsActive = true,
                CreatedAt= DateTime.UtcNow,
                StudentId = student.StudentId
            };
            context.Users.Add(user);
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
                throw new NotFoundException("Student not found");
            var emailExists = await context.Students.AnyAsync(s=>s.Email == request.Email && 
            s.StudentId != id && !s.IsDeleted);
            if (emailExists)
                throw new BusinessRuleException("Email already exists");
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
        public async Task<object?> UpdateMyProfile(UpdateMyStudentProfile request,ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrWhiteSpace(email))
                throw new UnauthorizedAccessException("Student identity not found");

            var student = await context.Students.FirstOrDefaultAsync(s => s.Email == email);
            if (student == null)
                throw new NotFoundException("Student account not found");
            if (string.IsNullOrWhiteSpace(request.FullName))
                throw new BusinessRuleException("Full name is required");
            
            student.FullName = request.FullName;
            student.PhoneNumber = request.Phone;
            await context.SaveChangesAsync();
            return new
            {
                student.StudentId,
                student.FullName,
                student.PhoneNumber,
                student.Email,
            };
        }
    }
}