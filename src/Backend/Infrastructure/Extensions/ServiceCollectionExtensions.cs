using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTracker.Infrastructure.Extensions;

public static class ServiceColectionExtension
{
    public static IServiceCollection AddTasksDb(
        this IServiceCollection services,
        IConfigurationBuilder configBuilder)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configBuilder);

        return services.AddDbContext<TasksDbContext>(
            dbContextOptions => dbContextOptions.UseTasksDb(configBuilder));
    }
}
