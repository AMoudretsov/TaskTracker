using System.Linq.Expressions;

namespace TaskTracker.Core.Interfaces.Db;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> FirstOrDefaultAsync(int id, CancellationToken cancelToken = default);

    Task<TProjection?> FirstOrDefaultAsync<TProjection>(
        int id,
        Expression<Func<TEntity, TProjection>> selector,
        CancellationToken cancelToken = default);

    Task<bool> ExistsAsync(int id, CancellationToken cancelToken = default);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancelToken = default);

    Task<List<TProjection>> ListAsync<TKey, TProjection>(
        Expression<Func<TEntity, TProjection>> selector,
        IEnumerable<Expression<Func<TEntity, bool>>?>? predicates = default,
        Expression<Func<TEntity, TKey>>? keySelector = default,
        int? limit = default,
        CancellationToken cancelToken = default);

    void Add(TEntity entity);

    void Attach(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}
