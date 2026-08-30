using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskTracker.Infrastructure.Common;
using TaskTracker.Infrastructure.Seeding;

namespace TaskTracker.Infrastructure.Extensions;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseTasksDb(
        this DbContextOptionsBuilder optionsBuilder,
        IConfigurationBuilder configurationBuilder,
        string environmentVarPrefix = "TASK_TRACKER_",
        string connectionStringKey = "TasksDb")
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);
        ArgumentNullException.ThrowIfNull(configurationBuilder);

        var config = configurationBuilder
            .AddEnvironmentVariables(environmentVarPrefix)
            .Build();

        var connectionString = config.GetConnectionString(connectionStringKey)
            ?? throw new InvalidOperationException($"Cannot find connection string `{connectionStringKey}`");

        return optionsBuilder.UseTasksDb(connectionString);
    }

    public static DbContextOptionsBuilder UseTasksDb(
        this DbContextOptionsBuilder optionsBuilder,
        string connectionString)
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);
        ArgumentNullException.ThrowIfNull(connectionString);

        return optionsBuilder
            .UseNpgsql(
                connectionString,
                pgDbContextOptions => pgDbContextOptions
                    .MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Tasks)
                    .EnableRetryOnFailure())
            .UseSnakeCaseNamingConvention()
            .EnableDetailedErrors()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseSeeding(TaskItemSeeding.Seed)
            .UseAsyncSeeding(TaskItemSeeding.SeedAsync);
    }
}
