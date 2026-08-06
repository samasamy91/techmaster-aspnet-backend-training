using Microsoft.AspNetCore.Identity;

namespace TrainingCenterAuthTask01.Data
{
    public class IdentitySeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                "Admin",
                "Instructor",
                "Student"
            };
            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
