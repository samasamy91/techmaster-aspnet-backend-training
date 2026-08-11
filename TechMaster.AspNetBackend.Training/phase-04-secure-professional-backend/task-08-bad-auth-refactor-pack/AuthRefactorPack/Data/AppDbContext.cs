using ActivityTrainingCenter.Entities;
using Microsoft.EntityFrameworkCore;
using TrainingCenterAuthTask01.Entities;

namespace TrainingCenter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        
        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Action).HasMaxLength(100).IsRequired();
                entity.Property(x => x.EntityName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.UserRole).HasMaxLength(50);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.IpAddress).HasMaxLength(45);
                entity.Property(x => x.Metadata).HasColumnType("nvarchar(max)");
                entity.Property(x => x.CreatedAt).IsRequired();

            });
        }

    }
}
