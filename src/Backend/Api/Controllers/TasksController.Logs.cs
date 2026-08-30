namespace TaskTracker.Api.Controllers;

public partial class TasksController
{
    private static class EventIds
    {
        public const int GetTaskById = 10001;
    }

    [LoggerMessage(
        EventId = EventIds.GetTaskById,
        EventName = nameof(EventIds.GetTaskById),
        Level = LogLevel.Information,
        Message = "Get task by id={Id}")]
    public partial void LogGetTaskById(int id);

}
