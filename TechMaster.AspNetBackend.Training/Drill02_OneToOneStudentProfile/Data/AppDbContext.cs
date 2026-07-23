using Drill02_OneToOneStudentProfile.Models;
using Microsoft.EntityFrameworkCore;

namespace Drill02_OneToOneStudentProfile.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students => Set<Student>(); 
        public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasOne(s => s.Profile).WithOne(p => p.Student)
                .HasForeignKey<StudentProfile>(p => p.StudentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
