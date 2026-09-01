using MediatR;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Commands.DeleteTask;

public record DeleteTaskCommand(int Id) : IRequest<OperationResult<DeleteTaskResponse?>>;
