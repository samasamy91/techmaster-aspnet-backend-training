using Drill01_DbContextFirstMigration.Models;
using Microsoft.EntityFrameworkCore;

namespace Drill01_DbContextFirstMigration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students => Set<Student>();
    }
}
