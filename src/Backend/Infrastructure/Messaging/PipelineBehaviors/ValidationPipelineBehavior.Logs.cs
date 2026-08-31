using Microsoft.Extensions.Logging;

namespace TaskTracker.Infrastructure.Messaging.PipelineBehaviors;

public partial class ValidationPipelineBehavior<TRequest, TOperationResult>
{
    private static class EventIds
    {
        public const int ValidationFailure = 30011;
    }

    [LoggerMessage(
        EventId = EventIds.ValidationFailure,
        EventName = nameof(EventIds.ValidationFailure),
        Level = LogLevel.Warning,
        Message = "Pipeline validation of {RequestType} failed: {ValidationMessages}")]
    public partial void LogValidationFailure(
        string? requestType,
        IReadOnlyDictionary<string, string[]> validationMessages);
}
