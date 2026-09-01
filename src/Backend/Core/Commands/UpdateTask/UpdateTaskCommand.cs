using MediatR;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Commands.UpdateTask;

public record UpdateTaskCommand(
    int Id,
    string? Title,
    string? Description,
    bool? IsCompleted,
    DateTime? CreatedAt
) : IRequest<OperationResult<UpdateTaskResponse?>>;
