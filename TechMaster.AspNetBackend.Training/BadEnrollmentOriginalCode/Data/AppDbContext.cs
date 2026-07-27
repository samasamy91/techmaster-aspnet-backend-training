using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Entities;

namespace TrainingCenter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<TrainingTrack> TrainingTracks => Set<TrainingTrack>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Payment> Payments => Set<Payment>();

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
            modelBuilder.Entity<Enrollment>().HasOne(e => e.Student).WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>().HasOne(e => e.TrainingTrack).WithMany(t => t.Enrollments).HasForeignKey(e => e.TrainingTrackId);

            modelBuilder.Entity<Enrollment>().HasIndex(e => new
            {
                e.StudentId,
                e.TrainingTrackId,
            }).IsUnique();

            //payment
            modelBuilder.Entity<Payment>().HasOne(p => p.Enrollment).WithMany(e => e.Payments).HasForeignKey(p => p.EnrollmentId);

            modelBuilder.Entity<Student>().HasData(

               new Student
               {
                   StudentId = 1,
                   FullName = "Ahmed Ali",
                   Email = "ahmed@test.com"
               },

               new Student
               {
                   StudentId = 2,
                   FullName = "Sara Mohamed",
                   Email = "sara@test.com"
               }
             );
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    InstructorId = 1,
                    FullName = "Ahmed Hassan",
                    Email = "ahmed@test.com"
                });
            modelBuilder.Entity<TrainingTrack>().HasData(

                new TrainingTrack
                {
                    TrainingTrackId = 1,
                    Title = "ASP.NET Backend",
                    Code = "ASPNET",
                    Capacity = 30,
                    InstructorId = 1
                },

                new TrainingTrack
                {
                    TrainingTrackId = 2,
                    Title = "Flutter",
                    Code = "FLUTTER",
                    Capacity = 20,
                    InstructorId = 1
                }
            );




        }

    }
}
