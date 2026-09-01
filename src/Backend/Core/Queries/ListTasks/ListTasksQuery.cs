using MediatR;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Queries.ListTasks;

public record ListTasksQuery(PagingQuery Paging) :
    IRequest<OperationResult<CollectionResponse<TaskResponse>?>>;
