namespace TaskTracker.Core.Commands.UpdateTask;

public record UpdateTaskResponse(
    int Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt);
