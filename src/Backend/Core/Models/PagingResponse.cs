namespace TaskTracker.Core.Models;

public record PagingResponse(int Limit, int? Cursor)
{
    public bool HasMore => Cursor != null;
}
