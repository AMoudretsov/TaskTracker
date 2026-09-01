using Microsoft.Extensions.Logging;
using MediatR;
using TaskTracker.Core.Entities;
using TaskTracker.Core.Interfaces.Db;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Commands.AddTask;

public partial class AddTaskHandler(
    ITaskRepository repository,
    IUnitOfWork uow,
    ILogger<AddTaskHandler> logger
) : IRequestHandler<AddTaskCommand, OperationResult<AddTaskResponse?>>
{
    public async Task<OperationResult<AddTaskResponse?>> Handle(
        AddTaskCommand command,
        CancellationToken cancelToken)
    {
        var isDuplicate = await repository.ExistsAsync(
            t => t.Title == command.Title,
            cancelToken);
        if (isDuplicate)
        {
            LogTaskTitleDuplicate(command.Title!);

            return OperationResult<AddTaskResponse?>.Failure(
                [new Error(ErrorCodes.InvalidInput, "Task title must be unique")]);
        }

        var task = new TaskItem
        {
            Title = command.Title!, // Title has been already validated and should have value at this point
            Description = command.Description
        };

        if (command.IsCompleted.HasValue)
        {
            task.IsCompleted = command.IsCompleted.Value;
        }

        if (command.CreatedAt.HasValue)
        {
            task.CreatedAt = DateTime.SpecifyKind(command.CreatedAt.Value, DateTimeKind.Utc);
        }

        repository.Add(task);

        await uow.CommitAsync(cancelToken);

        var response = await repository.FirstOrDefaultAsync(
            task.Id,
            t => new AddTaskResponse(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedAt),
            cancelToken);

        return OperationResult<AddTaskResponse?>.Success(response);
    }
}
