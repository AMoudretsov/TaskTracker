using System.Linq.Expressions;

namespace TaskTracker.Core.Interfaces.Db;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> FirstOrDefaultAsync(int id, CancellationToken cancelToken = default);

    Task<TProjection?> FirstOrDefaultAsync<TProjection>(
        int id,
        Expression<Func<TEntity, TProjection>> selector,
        CancellationToken cancelToken = default);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancelToken = default);

    void Add(TEntity entity);
}
