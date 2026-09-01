using Microsoft.Extensions.Logging;
using MediatR;
using TaskTracker.Core.Interfaces.Db;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Queries.GetTask;

public partial class GetTaskHandler(
    ITaskRepository repository,
    ILogger<GetTaskHandler> logger
) : IRequestHandler<GetTaskQuery, OperationResult<GetTaskResponse?>>
{
    public async Task<OperationResult<GetTaskResponse?>> Handle(GetTaskQuery request, CancellationToken cancelToken)
    {
        var task = await repository.FirstOrDefaultAsync(
            request.Id,
            t => new GetTaskResponse(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedAt),
            cancelToken);

        if (task == null)
        {
            LogTaskNotFound(request.Id);

            return OperationResult<GetTaskResponse?>.Failure(
                [new Error(ErrorCodes.NotFound, "Task not found")]);
        }

        return OperationResult<GetTaskResponse?>.Success(task);
    }
}
