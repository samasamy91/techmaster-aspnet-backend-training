using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenterAuthTask01.Entities;
using TrainingCenterAuthTask01.Entities.Enums;
using TrainingCenterAuthTask01.Security;

namespace TrainingCenterAuthorization.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedAdmin(AppDbContext context)
        {
            if (await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
                return;
            var password = new PasswordHasher();
            var admin = new User
            {
                FullName = "Admin",
                Email = "admin@test.com",
                HashPassword = password.Hash("Admin@123"),
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };
            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
