namespace TaskTracker.Core.Models;

public record CollectionResponse<TItem>(IEnumerable<TItem> Items, PagingResponse Paging);
