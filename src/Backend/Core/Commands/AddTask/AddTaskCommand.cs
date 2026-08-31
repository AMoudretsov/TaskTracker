using MediatR;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Commands.AddTask;

public record AddTaskCommand(
    string? Title,
    string? Description,
    bool? IsCompleted,
    DateTime? CreatedAt
) : IRequest<OperationResult<AddTaskResponse?>>;
