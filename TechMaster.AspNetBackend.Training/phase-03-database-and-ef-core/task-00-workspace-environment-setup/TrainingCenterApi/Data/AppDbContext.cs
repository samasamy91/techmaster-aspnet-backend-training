using Microsoft.EntityFrameworkCore;

namespace TrainingCenterApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
