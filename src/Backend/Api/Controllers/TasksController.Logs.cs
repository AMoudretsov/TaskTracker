using TaskTracker.Api.Models;

namespace TaskTracker.Api.Controllers;

public partial class TasksController
{
    private static class EventIds
    {
        public const int GetTaskById = 10002;
        public const int AddTask = 10003;
        public const int UpdateTask = 10004;
        public const int DeleteTask = 10005;
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

    [LoggerMessage(
        EventId = EventIds.UpdateTask,
        EventName = nameof(EventIds.UpdateTask),
        Level = LogLevel.Information,
        Message = "Update task {Model}")]
    public partial void LogUpdateTask(UpdateTaskModel model);

    [LoggerMessage(
        EventId = EventIds.DeleteTask,
        EventName = nameof(EventIds.DeleteTask),
        Level = LogLevel.Information,
        Message = "Delete task by id={Id}")]
    public partial void LogDeleteTask(int id);

}
