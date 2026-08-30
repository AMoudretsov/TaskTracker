using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Entities;
using TaskTracker.Infrastructure.Db.Extensions;

namespace TaskTracker.Infrastructure.Db;

public class TasksDbContext(DbContextOptions<TasksDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseIdentityAlwaysColumns()
            .AddCaseInsensitiveCollation()
            .ApplyConfigurationsFromAssembly(typeof(TasksDbContext).Assembly);
    }
}
