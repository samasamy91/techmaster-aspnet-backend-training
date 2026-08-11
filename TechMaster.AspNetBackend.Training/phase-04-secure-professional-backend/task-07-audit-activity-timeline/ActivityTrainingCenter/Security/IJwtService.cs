using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Entities;

namespace TrainingCenterAuthTask01.Security
{
    public interface IJwtService
    {
        TokenResponse GenerateToken(User user);
    }
}
