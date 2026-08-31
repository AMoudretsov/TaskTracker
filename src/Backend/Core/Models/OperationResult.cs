namespace TaskTracker.Core.Models;

public record OperationResult<TResponse>(
    TResponse? Response,
    IEnumerable<Error>? Errors = null
) : IOperationResult,
    IFailureFactory<OperationResult<TResponse>>
{
    public object? Value => Response;

    public bool IsSuccess => Errors == null || !Errors.Any();

    public static OperationResult<TResponse> Success(TResponse response) => new(response);

    public static OperationResult<TResponse> Failure(IEnumerable<Error> errors) => new(default, errors);
}
