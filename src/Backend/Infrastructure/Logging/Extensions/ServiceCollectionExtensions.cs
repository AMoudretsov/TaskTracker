using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TaskTracker.Infrastructure.Logging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(config);

        return services
            .AddHttpContextAccessor()
            .AddSerilog((serviceProvider, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(config)
                .ReadFrom.Services(serviceProvider)
                .Enrich.FromLogContext());
    }
}
