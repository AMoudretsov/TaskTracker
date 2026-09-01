using System.Linq.Expressions;
using MediatR;
using TaskTracker.Core.Entities;
using TaskTracker.Core.Interfaces.Db;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Queries.ListTasks;

public partial class ListTasksHandler(
    ITaskRepository repository
) : IRequestHandler<ListTasksQuery, OperationResult<CollectionResponse<TaskResponse>?>>
{
    private const int DefaultLimit = 100;

    public async Task<OperationResult<CollectionResponse<TaskResponse>?>> Handle(
        ListTasksQuery request,
        CancellationToken cancelToken)
    {
        var cursorPredicate = await GetCursorPredicateAsync(request.Paging.Cursor, cancelToken);

        var limit = request.Paging.Limit ?? DefaultLimit;

        var items = await repository.ListAsync(
            t => new TaskResponse(t.Id, t.Title, t.Description, t.IsCompleted, t.CreatedAt),
            cursorPredicate,
            t => t.CreatedAt,
            limit + 1,
            cancelToken);

        int? cursor = null;
        if (items.Count == limit + 1)
        {
            items.RemoveAt(items.Count - 1);
            cursor = items[^1].Id;
        }

        var pagingResponse = new PagingResponse(limit, cursor);
        var collectionResponse = new CollectionResponse<TaskResponse>(items, pagingResponse);

        return OperationResult<CollectionResponse<TaskResponse>?>.Success(collectionResponse);
    }

    private async Task<Expression<Func<TaskItem, bool>>?> GetCursorPredicateAsync(
        int? cursor,
        CancellationToken cancelToken = default)
    {
        if (!cursor.HasValue)
        {
            return null;
        }

        var cursorCreatedAt = await repository.FirstOrDefaultAsync(
            cursor.Value,
            t => t.CreatedAt,
            cancelToken);

        return task => task.CreatedAt > cursorCreatedAt
            || (task.CreatedAt == cursorCreatedAt && task.Id > cursor.Value);
    }
}
