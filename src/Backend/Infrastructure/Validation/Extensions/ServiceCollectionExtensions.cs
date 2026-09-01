using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using TaskTracker.Infrastructure.Validation.Adapters;
using App = TaskTracker.Core.Interfaces.Validation;

namespace TaskTracker.Infrastructure.Validation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped(typeof(App.IValidator<>), typeof(ValidatorAdapter<>));
    }
}
