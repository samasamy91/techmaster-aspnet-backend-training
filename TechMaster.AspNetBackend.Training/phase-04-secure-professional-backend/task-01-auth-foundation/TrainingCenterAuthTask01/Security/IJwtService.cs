using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Entities;

namespace TrainingCenterAuthTask01.Security
{
    public interface IJwtService
    {
        Task<TokenResponse> GenerateToken(ApplicationUser user);
    }
}
