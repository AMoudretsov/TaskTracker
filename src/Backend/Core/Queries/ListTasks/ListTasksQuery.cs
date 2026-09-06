using MediatR;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Queries.ListTasks;

public record ListTasksQuery(string? Search, PagingQuery Paging) :
    IRequest<OperationResult<CollectionResponse<TaskResponse>?>>;
