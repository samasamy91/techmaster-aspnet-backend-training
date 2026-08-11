using ActivityTrainingCenter.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TrainingCenter.Api.Data;
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
        private readonly IActivityLogService logService;

        public AuthService(AppDbContext context, PasswordHasher passwordHasher, IJwtService jwtService, ILogger<AuthService> logger, IActivityLogService logService)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.jwtService = jwtService;
            this.logger = logger;
            this.logService = logService;
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

            if (request.Role == UserRole.Instructor.ToString())
            {
                if (string.IsNullOrWhiteSpace(request.Specialization))
                    throw new BusinessRuleException("Specialization required for instructor");
            }
            using var transaction = await context.Database.BeginTransactionAsync();
            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                HashPassword = passwordHasher.Hash(request.Password),
                Role = role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            await logService.Log(null, "User Registered", "User", user.Id, $"User {user.Email} registered with role {user.Role}");
            await transaction.CommitAsync();

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,

            };
            
        }
        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

            if (user == null)
            {
                logger.LogWarning("Login failed for email: {Email}", request.Email);
                await logService.Log(null,"Login Failed","User",null,"Login attempt failed.");
                throw new BusinessRuleException("Invalid email or password");
            }


            if (!user.IsActive)
            {
                await logService.Log(null,"Login Failed","User",user.Id,"Login attempt for inactive user.");
                throw new BusinessRuleException("User Account is inactive");
            }

            if (!passwordHasher.Verify(request.Password, user.HashPassword))
            {
                logger.LogWarning("Login failed for email: {Email}", request.Email);
                await logService.Log(null, "Login Failed", "User", user.Id, "Login attempt failed due to invalid credenials");
                throw new BusinessRuleException("Invalid email or Password");
            }

            user.LastLoginAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            var token = jwtService.GenerateToken(user);
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Role,
                    user.Role.ToString())
            });
            var principal = new ClaimsPrincipal(identity);
            await logService.Log(principal, "User Logged In", "User", user.Id, $"User {user.Email} logged in");
            logger.LogInformation("Login successful for UserId: {UserId}", user.Id);


            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
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

            if (!int.TryParse(userId, out int id))
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
            if (!passwordHasher.Verify(request.CurrentPassword, user.HashPassword))
                throw new BusinessRuleException("Current password not correct");
            if (request.NewPassword != request.ConfirmPassword)
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
