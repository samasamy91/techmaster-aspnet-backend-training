using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Threading.Tasks;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities;
using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Entities.Enums;
using TrainingCenterAuthTask01.Security;
using TrainingCenterAuthTask01.Services.IServices;
using ValidationTrainingCenter.Common.Exceptions;

namespace TrainingCenterAuthTask01.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly PasswordHasher passwordHasher;
        private readonly IJwtService jwtService;
        private readonly ILogger<AuthService> logger;

        public AuthService(AppDbContext context,PasswordHasher passwordHasher,IJwtService jwtService, ILogger<AuthService> logger)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.jwtService = jwtService;
            this.logger = logger;
        }
        public async Task<AuthResponse> Register(RegisterRequest request)
        {

            if (request.Password != request.ConfirmPassword)
                throw new BusinessRuleException("Passwords dont match");

            var emailExists = await context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailExists)
                throw new BusinessRuleException("Email already exists");

            if (!Enum.TryParse<UserRole>(request.Role, true, out var role))
                throw new BusinessRuleException("Invalid role");

            if (role == UserRole.Admin)
                throw new BusinessRuleException("Cannot register as Admin");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new BusinessRuleException("Password is required");

            if (request.Password.Length < 8)
                throw new BusinessRuleException("Password must be at least 8 characters");

            if (!request.Password.Any(char.IsUpper))
                throw new BusinessRuleException("Password must contain at least one uppercase letter");

            if (!request.Password.Any(char.IsLower))
                throw new BusinessRuleException("Password must contain at least one lowercase letter");

            if (!request.Password.Any(char.IsDigit))
                throw new BusinessRuleException("Password must contain at least one digit");

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
                        throw new BusinessRuleException("Specialization required for instructor");
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
            }catch(Exception)
            {
                throw new Exception("Error in registration");
            }
        }
        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (user == null)
            {
                logger.LogWarning("Login failed for email: {Email}", request.Email);
                throw new BusinessRuleException("Invalid email or password");
            }
                
            if (!user.IsActive)
                throw new BusinessRuleException("User Account is inactive");
           
            if (!passwordHasher.Verify(request.Password, user.HashPassword))
            {
                logger.LogWarning("Login failed for email: {Email}", request.Email);
                throw new BusinessRuleException("Invalid Password");
            }
                
            user.LastLoginAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            var token = jwtService.GenerateToken(user);
            logger.LogInformation("Login successfull for UserId: {UserId}", user.Id);

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
                throw new UnauthorizedAccessException("Invalid Token");

            if(!int.TryParse(userId, out int id))
                throw new UnauthorizedAccessException("Invalid user id");
            var user = await context.Users.FindAsync(id);
            if (user == null)
                throw new NotFoundException("User not found");

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
                throw new UnauthorizedAccessException("Invalid token");
            if (!int.TryParse(userId, out int id))
                throw new UnauthorizedAccessException("Invalid user id");
            var user = await context.Users.FindAsync(id);
            if (user == null)
                throw new NotFoundException("User not found");
            if(!passwordHasher.Verify(request.CurrentPassword,user.HashPassword))
                throw new BusinessRuleException("Current password not correct");
            if(request.NewPassword != request.ConfirmPassword)
                throw new BusinessRuleException("Passwords dont match");
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
