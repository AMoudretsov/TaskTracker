using Microsoft.Extensions.Logging;

namespace TaskTracker.Core.Commands.UpdateTask;

public partial class UpdateTaskHandler
{
    private static class EventIds
    {
        public const int TaskNotFound = 20041;
        public const int TaskTitleDuplicate = 20042;
    }

    [LoggerMessage(
        EventId = EventIds.TaskNotFound,
        EventName = nameof(EventIds.TaskNotFound),
        Level = LogLevel.Warning,
        Message = "Task with id={Id} not found")]
    public partial void LogTaskNotFound(int id);

    [LoggerMessage(
        EventId = EventIds.TaskTitleDuplicate,
        EventName = nameof(EventIds.TaskTitleDuplicate),
        Level = LogLevel.Warning,
        Message = "Task with title=\"{Title}\" already exists")]
    public partial void LogTaskTitleDuplicate(string title);
}
