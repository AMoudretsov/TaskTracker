using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Interfaces.Db;

namespace TaskTracker.Infrastructure.Db.Repositories;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class, IEntity
{
    protected IQueryable<TEntity> Entities => context.Set<TEntity>();

    public virtual Task<TEntity?> FirstOrDefaultAsync(int id, CancellationToken cancelToken = default)
    {
        return Entities.FirstOrDefaultAsync(
            entity => entity.Id == id,
            cancelToken);
    }

    public virtual Task<TProjection?> FirstOrDefaultAsync<TProjection>(
        int id,
        Expression<Func<TEntity, TProjection>> selector,
        CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return Entities
            .Where(entity => entity.Id == id)
            .Select(selector)
            .FirstOrDefaultAsync(cancelToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancelToken = default)
    {
        return ExistsAsync(
            entity => entity.Id == id,
            cancelToken);
    }

    public Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return Entities.AnyAsync(predicate, cancelToken);
    }

    public void Add(TEntity entity)
    {
        context.Add(entity);
    }

    public void Attach(TEntity entity)
    {
        context.Attach(entity);
    }

    public void Update(TEntity entity)
    {
        context.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        context.Remove(entity);
    }
}
