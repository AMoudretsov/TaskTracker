using Microsoft.Extensions.Logging;
using MediatR;
using TaskTracker.Core.Interfaces.Db;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Commands.UpdateTask;

public partial class UpdateTaskHandler(
    ITaskRepository repository,
    IUnitOfWork uow,
    ILogger<UpdateTaskHandler> logger
) : IRequestHandler<UpdateTaskCommand, OperationResult<UpdateTaskResponse?>>
{
    public async Task<OperationResult<UpdateTaskResponse?>> Handle(
        UpdateTaskCommand command,
        CancellationToken cancelToken)
    {
        var task = await repository.FirstOrDefaultAsync(command.Id, cancelToken);
        if (task == null)
        {
            LogTaskNotFound(command.Id);

            return OperationResult<UpdateTaskResponse?>.Failure(
                [new Error(ErrorCodes.NotFound, "Task not found")]);
        }

        var isDuplicate = await repository.ExistsAsync(
            t => t.Title == command.Title && t.Id != command.Id,
            cancelToken);
        if (isDuplicate)
        {
            LogTaskTitleDuplicate(command.Title!);

            return OperationResult<UpdateTaskResponse?>.Failure(
                [new Error(ErrorCodes.InvalidInput, "Task title must be unique")]);
        }

        repository.Attach(task);

        // Applying ! operator to the nullable properties is safe at this point,
        // since they have been already validated and should have values.
        task.Title = command.Title!;
        task.Description = command.Description;
        task.IsCompleted = command.IsCompleted!.Value;
        task.CreatedAt = DateTime.SpecifyKind(command.CreatedAt!.Value, DateTimeKind.Utc);

        await uow.CommitAsync(cancelToken);

        var response = await repository.FirstOrDefaultAsync(
            task.Id,
            t => new UpdateTaskResponse(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedAt),
            cancelToken);

        return OperationResult<UpdateTaskResponse?>.Success(response);
    }
}
