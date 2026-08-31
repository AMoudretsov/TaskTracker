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
            x => x.Id == id,
            cancelToken);
    }

    public virtual Task<TProjection?> FirstOrDefaultAsync<TProjection>(
        int id,
        Expression<Func<TEntity, TProjection>> selector,
        CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return Entities
            .Where(x => x.Id == id)
            .Select(selector)
            .FirstOrDefaultAsync(cancelToken);
    }
}
