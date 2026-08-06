using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainingCenterAuthTask01.DTOs.Auth;
using TrainingCenterAuthTask01.Entities;

namespace TrainingCenterAuthTask01.Security
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings settings;
        private readonly UserManager<ApplicationUser> userManager;
        public JwtService(IOptions<JwtSettings> options,UserManager<ApplicationUser> userManager)
        {
            settings = options.Value;
            this.userManager = userManager;
        } 
        public async Task<TokenResponse> GenerateToken(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
           
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(settings.ExpirationMinutes);
            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
                );
            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires
            };
        }
    }
}
