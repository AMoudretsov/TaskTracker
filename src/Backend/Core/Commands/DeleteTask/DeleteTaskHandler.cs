using Microsoft.Extensions.Logging;
using MediatR;
using TaskTracker.Core.Entities;
using TaskTracker.Core.Interfaces.Db;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Commands.DeleteTask;

public partial class DeleteTaskHandler(
    ITaskRepository repository,
    IUnitOfWork uow,
    ILogger<DeleteTaskHandler> logger
) : IRequestHandler<DeleteTaskCommand, OperationResult<DeleteTaskResponse?>>
{
    public async Task<OperationResult<DeleteTaskResponse?>> Handle(
        DeleteTaskCommand command,
        CancellationToken cancelToken)
    {
        var exists = await repository.ExistsAsync(command.Id, cancelToken);
        if (!exists)
        {
            LogTaskNotFound(command.Id);

            return OperationResult<DeleteTaskResponse?>.Failure(
                [new Error(ErrorCodes.NotFound, "Task not found")]);
        }

        repository.Remove(new TaskItem { Id = command.Id, Title = "Dummy value" });

        await uow.CommitAsync(cancelToken);

        return OperationResult<DeleteTaskResponse?>.Success(null);
    }
}
