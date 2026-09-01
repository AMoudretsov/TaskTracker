using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Core.Entities;
using TaskTracker.Infrastructure.Messaging.PipelineBehaviors;

namespace TaskTracker.Infrastructure.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInProcessMessaging(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<TaskItem>()
            .AddOpenBehavior(typeof(CatchAllPipelineBehavior<,>))
            .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>)));
    }
}
