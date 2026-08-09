using Microsoft.EntityFrameworkCore;
using SecurePlatformUpgrade.Entities;
using TrainingCenter.Api.Entities;
using TrainingCenterAuthTask01.Entities;

namespace TrainingCenter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<TrainingTrack> TrainingTracks => Set<TrainingTrack>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<TrackSession> TrackSessions => Set<TrackSession>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //student 
            modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();

            //Instructor
            modelBuilder.Entity<Instructor>().HasIndex(i=>i.Email).IsUnique();

            //TrainingTrack
            modelBuilder.Entity<TrainingTrack>().HasIndex(t => t.Code).IsUnique();

            modelBuilder.Entity<TrainingTrack>().HasOne(t => t.Instructor).WithMany(i => i.TrainingTracks).HasForeignKey(t => t.InstructorId).OnDelete(DeleteBehavior.Restrict);

            //Enrollment
            modelBuilder.Entity<Enrollment>().HasOne(e => e.Student).WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>().HasOne(e=>e.TrainingTrack).WithMany(t=>t.Enrollments).HasForeignKey(e=>e.TrainingTrackId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>().Property(e => e.ProgressPercentage).HasPrecision(5, 2);

            modelBuilder.Entity<TrainingTrack>().Property(t => t.Fee).HasPrecision(18, 2);

            //modelBuilder.Entity<Enrollment>().HasIndex(e => new
            //{
            //    e.StudentId,
            //    e.TrainingTrackId,
            //}).IsUnique();

            //payment
            modelBuilder.Entity<Payment>().HasOne(p => p.Enrollment).WithMany(e => e.Payments).HasForeignKey(p => p.EnrollmentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>().HasIndex(p=>p.ReferenceNumber).IsUnique();

            modelBuilder.Entity<TrackSession>().HasKey(s => s.TrackSessionId);

            modelBuilder.Entity<TrackSession>().HasOne(s => s.TrainingTrack).WithMany().HasForeignKey(s => s.TrainingTrackId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrackSession>().HasOne(s => s.CreatedByInstructor).WithMany().HasForeignKey(s => s.CreatedByInstructorId).OnDelete(DeleteBehavior.Restrict);


        }

    }
}
