using Microsoft.Extensions.Logging;

namespace TaskTracker.Core.Commands.AddTask;

public partial class AddTaskHandler
{
    private static class EventIds
    {
        public const int TaskTitleDuplicate = 20021;
    }

    [LoggerMessage(
        EventId = EventIds.TaskTitleDuplicate,
        EventName = nameof(EventIds.TaskTitleDuplicate),
        Level = LogLevel.Warning,
        Message = "Task with title=\"{Title}\" already exists")]
    public partial void LogTaskTitleDuplicate(string title);
}
