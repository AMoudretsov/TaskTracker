using Microsoft.AspNetCore.Builder;
using Serilog;

namespace TaskTracker.Infrastructure.Logging.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.UseSerilogRequestLogging();
    }
}
