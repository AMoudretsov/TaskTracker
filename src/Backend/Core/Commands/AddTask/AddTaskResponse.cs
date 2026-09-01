namespace TaskTracker.Core.Commands.AddTask;

public record AddTaskResponse(
    int Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt);
