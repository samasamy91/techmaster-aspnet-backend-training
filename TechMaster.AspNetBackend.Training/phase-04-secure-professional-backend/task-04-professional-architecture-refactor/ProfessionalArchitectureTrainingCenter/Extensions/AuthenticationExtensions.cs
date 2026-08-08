using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrainingCenterAuthTask01.Security;

namespace ProfessionalArchitectureTrainingCenter.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));
            var jwtSettings = config.GetSection(JwtSettings.SectionName).Get<JwtSettings>()?? throw new InvalidOperationException("JWT settings are not cofigured correctly");
            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };

            });
            //Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly",
                    p => p.RequireRole("Admin"));
                options.AddPolicy("InstructorOnly",
                    p => p.RequireRole("Instructor"));
                options.AddPolicy("StudentOnly",
                    p => p.RequireRole("Student"));
                options.AddPolicy("InstructorOrAdmin",
                    p => p.RequireRole("Instructor", "Admin"));
            });
            return services;
        }
    }
}
