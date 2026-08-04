using Drill02_OneToOneStudentProfile.Models;
using Drill03_OneToManyInstructorTracks.Models;
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
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<TrainingTrack> TrainingTracks => Set<TrainingTrack>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasOne(s => s.Profile).WithOne(p => p.Student)
                .HasForeignKey<StudentProfile>(p => p.StudentId);

            modelBuilder.Entity<TrainingTrack>().HasOne(t => t.Instructor)
                .WithMany(i => i.TrainingTracks).HasForeignKey(t => t.InstructorId).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
