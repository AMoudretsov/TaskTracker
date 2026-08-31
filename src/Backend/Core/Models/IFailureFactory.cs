namespace TaskTracker.Core.Models;

public interface IFailureFactory<T>
{
    static abstract T Failure(IEnumerable<Error> errors);
}
