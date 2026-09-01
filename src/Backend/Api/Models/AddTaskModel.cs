using TaskTracker.Core.Commands.AddTask;

namespace TaskTracker.Api.Models;

public record AddTaskModel(
    string? Title,
    string? Description,
    bool? IsCompleted,
    DateTime? CreatedAt)
{
    public AddTaskCommand ToCommand() => new(Title, Description, IsCompleted, CreatedAt);
}
