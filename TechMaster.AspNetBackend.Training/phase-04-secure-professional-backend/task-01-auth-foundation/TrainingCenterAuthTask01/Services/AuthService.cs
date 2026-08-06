using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Security;
using TrainingCenterAuthTask01.Services.IServices;

namespace TrainingCenterAuthTask01.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJwtService jwtService;

        public AuthService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }
        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new BadHttpRequestException("Passwords dont match");
            var emailExists = await userManager.FindByEmailAsync(request.Email);
            if (emailExists != null)
                throw new BadHttpRequestException("Email already exists");
            if (request.Role != "Student")
                throw new BadHttpRequestException("Only student registration is allowed");
            var user = new ApplicationUser
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.Email,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new BadHttpRequestException(result.Errors.First().Description);
            await userManager.AddToRoleAsync(user, "Student");
            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = "Student",
                AccessToken = "",
                ExpiresAt = DateTime.UtcNow,
            };
        }
        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new BadHttpRequestException("Invalid email or password");
            if (!user.IsActive)
                throw new BadHttpRequestException("User Account is inactive");
            var validPassword = await userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
                throw new BadHttpRequestException("Invalid Password");
            var roles = await userManager.GetRolesAsync(user);
            user.LastLoginAt = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            var token = await jwtService.GenerateToken(user);
            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "",
                AccessToken = token.Token,
                ExpiresAt = token.Expiration,
            };
        }
        public async Task<CurrentUserResponse?> GetCurrentUser(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new BadHttpRequestException("Invalid token");
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
                return null;
            var roles = await userManager.GetRolesAsync(user);
            return new CurrentUserResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "",
                LinkedStudentId = user.StudentId,
                LinkkedInstructorId = user.InstructorId
            };
        }
        public async Task ChangePassword(ClaimsPrincipal principal, ChangePasswordRequest request)
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new BadHttpRequestException("Invalid token");
            if (request.NewPassword != request.ConfirmPassword)
                throw new BadHttpRequestException("Passwords dont match");
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new BadHttpRequestException("User not found");
            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            
            if (!result.Succeeded)
                throw new BadHttpRequestException(result.Errors.First().Description);
        }
        public Task Logout()
        {
            return Task.CompletedTask;
        }
    }
}
