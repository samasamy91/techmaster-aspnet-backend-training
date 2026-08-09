using Microsoft.Extensions.DependencyInjection;
using TrainingCenter.Api.Services;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterAuthTask01.Security;
using TrainingCenterAuthTask01.Services;
using TrainingCenterAuthTask01.Services.IServices;

namespace ProfessionalArchitectureTrainingCenter
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<PasswordHasher>();

            return services;
        }
    }
}
