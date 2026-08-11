using ActivityTrainingCenter.Services;
using ActivityTrainingCenter.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using TrainingCenterAuthTask01.Security;
using TrainingCenterAuthTask01.Services;
using TrainingCenterAuthTask01.Services.IServices;

namespace ProfessionalArchitectureTrainingCenter
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<PasswordHasher>();

            return services;
        }
    }
}
