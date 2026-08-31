using TaskTracker.Api.Models;

namespace TaskTracker.Api.Controllers;

public partial class TasksController
{
    private static class EventIds
    {
        public const int GetTaskById = 10002;
        public const int AddTask = 10003;
    }

    [LoggerMessage(
        EventId = EventIds.GetTaskById,
        EventName = nameof(EventIds.GetTaskById),
        Level = LogLevel.Information,
        Message = "Get task by id={Id}")]
    public partial void LogGetTaskById(int id);

    [LoggerMessage(
        EventId = EventIds.AddTask,
        EventName = nameof(EventIds.AddTask),
        Level = LogLevel.Information,
        Message = "Add task {Model}")]
    public partial void LogAddTask(AddTaskModel model);
}
