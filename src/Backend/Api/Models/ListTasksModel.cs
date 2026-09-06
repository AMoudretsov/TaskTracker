using TaskTracker.Core.Models;
using TaskTracker.Core.Queries.ListTasks;

namespace TaskTracker.Api.Models;

public record ListTasksModel(
    string? Search,
    int? Limit,
    int? Cursor
)
{
    public ListTasksQuery ToQuery() => new(Search, new PagingQuery(Limit, Cursor));
}
