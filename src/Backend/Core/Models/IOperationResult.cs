namespace TaskTracker.Core.Models;

public interface IOperationResult
{
    object? Value { get; }

    IEnumerable<Error>? Errors { get; }

    public bool IsSuccess { get; }
}
