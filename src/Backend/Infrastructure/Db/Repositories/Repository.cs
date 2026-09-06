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

    public Task<List<TProjection>> ListAsync<TKey, TProjection>(
        Expression<Func<TEntity, TProjection>> selector,
        IEnumerable<Expression<Func<TEntity, bool>>?>? predicates = default,
        Expression<Func<TEntity, TKey>>? keySelector = default,
        int? limit = default,
        CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        var qry = Entities;

        if (predicates is not null)
        {
            foreach (var predicate in predicates)
            {
                if (predicate is null)
                {
                    continue;
                }

                qry = qry.Where(predicate);
            }
        }

        if (keySelector is not null)
        {
            qry = qry
                .OrderBy(keySelector)
                .ThenBy(entity => entity.Id);
        }
        else
        {
            qry = qry.OrderBy(entity => entity.Id);
        }

        if (limit.HasValue)
        {
            qry = qry.Take(limit.Value);
        }

        return qry
            .Select(selector)
            .ToListAsync(cancelToken);
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
