using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Common;
using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Services.IServices;

namespace TrainingCenterAuthTask01.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(DTOs.Auth.RegisterRequest request)
        {
            var result = await authService.Register(request);
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(result, "User registered successfully"));
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(DTOs.Auth.LoginRequest request)
        {
            var result = await authService.Login(request);
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(result, "Login successful"));
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var result = await authService.GetCurrentUser(User);
            return Ok(ApiResponse<CurrentUserResponse>.SuccessResponse(result, "Current user retrieved successfully"));
        }
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            await authService.ChangePassword(User, request);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Password changed successfully"));
        }
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(ApiResponse<string>.SuccessResponse(null, "Logged out successfully"));
        }
    }
}
