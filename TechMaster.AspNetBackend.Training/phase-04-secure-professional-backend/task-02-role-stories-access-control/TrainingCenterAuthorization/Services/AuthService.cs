using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities;
using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Entities.Enums;
using TrainingCenterAuthTask01.Security;
using TrainingCenterAuthTask01.Services.IServices;

namespace TrainingCenterAuthTask01.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly PasswordHasher passwordHasher;
        private readonly IJwtService jwtService;

        public AuthService(AppDbContext context,PasswordHasher passwordHasher,IJwtService jwtService)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.jwtService = jwtService;
        }
        public async Task<AuthResponse> Register(RegisterRequest request)
        {

            if (request.Password != request.ConfirmPassword)
                throw new BadHttpRequestException("Passwords dont match");

            var emailExists = await context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailExists)
                throw new BadHttpRequestException("Email already exists");

            if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
                throw new BadHttpRequestException("Invalid role");

            if (role == UserRole.Admin)
                throw new BadHttpRequestException("Students cannot register as Admin");
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                int? studentId = null;
                int? instructorId = null;
                if (request.Role == UserRole.Student.ToString())
                {
                    var student = new Student
                    {
                        FullName = request.FullName,
                        Email = request.Email,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    context.Students.Add(student);
                    await context.SaveChangesAsync();
                    studentId = student.StudentId;
                }
                else if (request.Role == UserRole.Instructor.ToString())
                {
                    if (string.IsNullOrWhiteSpace(request.Specialization))
                        throw new BadImageFormatException("Specialization required for instructor");
                    var instructor = new Instructor
                    {
                        FullName = request.FullName,
                        Email = request.Email,
                        Specialization = request.Specialization,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    context.Instructors.Add(instructor);
                    await context.SaveChangesAsync();
                    instructorId = instructor.InstructorId;
                }
                var user = new User
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    HashPassword = passwordHasher.Hash(request.Password),
                    Role = role,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    StudentId = studentId,
                    InstructorId = instructorId
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new AuthResponse
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,

                };
            }catch(BadHttpRequestException)
            {
                throw new BadHttpRequestException("Error in registration");
            }
        }
        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (user == null)
                throw new BadHttpRequestException("Invalid email or password");
            if (!user.IsActive)
                throw new BadHttpRequestException("User Account is inactive");
           
            if (!passwordHasher.Verify(request.Password, user.HashPassword))
                throw new BadHttpRequestException("Invalid Password");
            user.LastLoginAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            var token = jwtService.GenerateToken(user);
            return new AuthResponse
            {
                UserId = user.Id,
                FullName= user.FullName,
                Email = user.Email,
                Role = user.Role,
                AccessToken = token.Token,
                ExpiresAt = token.Expiration
            };
        }
        public async Task<CurrentUserResponse?> GetCurrentUser(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new BadHttpRequestException("Invalid Token");

            if(!int.TryParse(userId, out int id))
                throw new BadHttpRequestException("Invalid user id");
            var user = await context.Users.FindAsync(id);
            if (user == null)
                throw new BadHttpRequestException("User not found");

            return new CurrentUserResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                LinkedStudentId = user.StudentId,
                LinkkedInstructorId = user.InstructorId
            };
        }
        public async Task ChangePassword(ClaimsPrincipal principal, ChangePasswordRequest request)
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new BadHttpRequestException("Invalid token");
            if (!int.TryParse(userId, out int id))
                throw new BadHttpRequestException("Invalid user id");
            var user = await context.Users.FindAsync(id);
            if (user == null)
                throw new BadHttpRequestException("User not found");
            if(!passwordHasher.Verify(request.CurrentPassword,user.HashPassword))
                throw new BadHttpRequestException("Current password not correct");
            if(request.NewPassword != request.ConfirmPassword)
                throw new BadHttpRequestException("Passwords dont match");
            user.HashPassword = passwordHasher.Hash(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            
        }
        public Task Logout()
        {
            return Task.CompletedTask;
        }
    }
}
