namespace TaskTracker.Core.Queries.GetTask;

public record GetTaskResponse(
    int Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt);
