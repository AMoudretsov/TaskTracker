using TaskTracker.Core.Commands.UpdateTask;

namespace TaskTracker.Api.Models;

public record UpdateTaskModel(
    string? Title,
    string? Description,
    bool? IsCompleted,
    DateTime? CreatedAt)
{
    public UpdateTaskCommand ToCommand(int id) => new(id, Title, Description, IsCompleted, CreatedAt);
};
