using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Security;

namespace TrainingCenterAuthTask01.Services.IServices
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(RegisterRequest request);
        Task<AuthResponse> Login(LoginRequest request);
        Task Logout();
        Task<CurrentUserResponse?> GetCurrentUser(ClaimsPrincipal principal);
        Task ChangePassword(ClaimsPrincipal principal, ChangePasswordRequest request);
    }
}
