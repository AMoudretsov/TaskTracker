using TaskTracker.Api.ActionFilters;

namespace TaskTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddControllersAndGlobalFilters(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddControllers(options =>
        {
            options.Filters.AddService<OperationResultActionFilter>();
        });

        return services.AddScoped<OperationResultActionFilter>();
    }

    public static IServiceCollection AddLowercaseUrls(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }
}
