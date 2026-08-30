namespace TaskTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
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
