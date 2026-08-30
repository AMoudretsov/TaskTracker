using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TaskTracker.Infrastructure.Extensions;

namespace TaskTracker.Infrastructure;

public class TasksDbContextFactory : IDesignTimeDbContextFactory<TasksDbContext>
{
    public TasksDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TasksDbContext>();
        optionsBuilder.UseTasksDb(new ConfigurationBuilder());

        return new TasksDbContext(optionsBuilder.Options);
    }
}
