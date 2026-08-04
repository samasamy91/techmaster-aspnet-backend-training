using Drill02_OneToOneStudentProfile.Models;
using Drill03_OneToManyInstructorTracks.Models;
using Drill04_ManyToManyEnrollment.Models;
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
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasOne(s => s.Profile).WithOne(p => p.Student)
                .HasForeignKey<StudentProfile>(p => p.StudentId);

            modelBuilder.Entity<TrainingTrack>().HasOne(t => t.Instructor)
                .WithMany(i => i.TrainingTracks).HasForeignKey(t => t.InstructorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>().HasOne(e => e.Student)
                .WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>().HasOne(e => e.TrainingTrack)
                .WithMany(t => t.Enrollments).HasForeignKey(e => e.TrainingTrackId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
