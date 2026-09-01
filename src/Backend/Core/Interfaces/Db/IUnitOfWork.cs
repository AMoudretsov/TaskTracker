namespace TaskTracker.Core.Interfaces.Db;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancelToken = default);
}
