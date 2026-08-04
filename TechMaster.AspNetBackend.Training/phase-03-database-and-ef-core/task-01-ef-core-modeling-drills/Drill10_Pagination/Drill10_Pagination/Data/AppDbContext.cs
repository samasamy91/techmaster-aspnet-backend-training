using Drill02_OneToOneStudentProfile.Models;
using Drill03_OneToManyInstructorTracks.Models;
using Drill04_ManyToManyEnrollment.Models;
using Drill05_PaymentSummary.Models;
using Drill08_AuditFields.Models;
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
        public DbSet<PaymentSummary> PaymentSummaries => Set<PaymentSummary>();

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

            modelBuilder.Entity<PaymentSummary>().HasOne(p => p.Enrollment)
                .WithOne(e => e.PaymentSummary).HasForeignKey<PaymentSummary>(p => p.EnrollmentId);

            modelBuilder.Entity<PaymentSummary>().Property(p => p.TotalRequired).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PaymentSummary>().Property(p => p.TotalPaid).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Ahmed Ali",
                    Email = "ahmed@example.com"
                },
                new Student
                {
                    Id = 2,
                    Name = "Sara Mohamed",
                    Email = "sara@example.com"
                },
                new Student
                {
                    Id = 3,
                    Name = "Omar Hassan",
                    Email = "omar@example.com"
                },
                new Student
                {
                    Id = 4,
                    Name = "Mona Ibrahim",
                    Email = "mona@example.com"
                },
                new Student
                {
                    Id = 5,
                    Name = "Youssef Mahmoud",
                    Email = "youssef@example.com"
                }
            );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = 1,
                    Name = "Mohamed Hassan",
                    Email = "mohamed@academy.com"
                },
                new Instructor
                {
                    Id = 2,
                    Name = "Nour Ahmed",
                    Email = "nour@academy.com"
                }
            );

            modelBuilder.Entity<TrainingTrack>().HasData(
                new TrainingTrack
                {
                    Id = 1,
                    Name = "ASP.NET Core",
                    DurationInMonths = 6,
                    InstructorId = 1
                },
                new TrainingTrack
                {
                    Id = 2,
                    Name = "Entity Framework Core",
                    DurationInMonths = 2,
                    InstructorId = 1
                },
                new TrainingTrack
                {
                    Id = 3,
                    Name = "SQL Server",
                    DurationInMonths = 3,
                    InstructorId = 2
                }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    Id = 1,
                    StudentId = 1,
                    TrainingTrackId = 1,
                    Status = "Active",
                    EnrollmentDate = new DateTime(2026, 7, 1)
                },
                new Enrollment
                {
                    Id = 2,
                    StudentId = 2,
                    TrainingTrackId = 1,
                    Status = "Active",
                    EnrollmentDate = new DateTime(2026, 7, 2)
                },
                new Enrollment
                {
                    Id = 3,
                    StudentId = 3,
                    TrainingTrackId = 2,
                    Status = "Completed",
                    EnrollmentDate = new DateTime(2026, 6, 15)
                },
                new Enrollment
                {
                    Id = 4,
                    StudentId = 4,
                    TrainingTrackId = 3,
                    Status = "Active",
                    EnrollmentDate = new DateTime(2026, 7, 5)
                },
                new Enrollment
                {
                    Id = 5,
                    StudentId = 5,
                    TrainingTrackId = 2,
                    Status = "Pending",
                    EnrollmentDate = new DateTime(2026, 7, 10)
                }
            );

            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

    }
}
