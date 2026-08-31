using Microsoft.Extensions.Logging;

namespace TaskTracker.Infrastructure.Messaging.PipelineBehaviors;

public partial class CatchAllPipelineBehavior<TRequest, TOperationResult>
{
    private static class EventIds
    {
        public const int UnhandledException = 30001;
    }

    [LoggerMessage(
        EventId = EventIds.UnhandledException,
        EventName = nameof(EventIds.UnhandledException),
        Level = LogLevel.Error,
        Message = "Unhandled exception in the Core pipeline")]
    public partial void LogUnhandledException(Exception ex);
}
