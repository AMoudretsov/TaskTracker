using Microsoft.Extensions.Logging;

namespace TaskTracker.Core.Queries.GetTask;

public partial class GetTaskHandler
{
    private static class EventIds
    {
        public const int TaskNotFound = 20011;
    }

    [LoggerMessage(
        EventId = EventIds.TaskNotFound,
        EventName = nameof(EventIds.TaskNotFound),
        Level = LogLevel.Warning,
        Message = "Task with id={Id} not found")]
    public partial void LogTaskNotFound(int id);
}
