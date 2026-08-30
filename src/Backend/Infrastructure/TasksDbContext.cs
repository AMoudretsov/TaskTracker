using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Entities;
using TaskTracker.Infrastructure.Extensions;

namespace TaskTracker.Infrastructure;

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
