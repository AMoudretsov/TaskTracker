using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Interfaces.Db;

namespace TaskTracker.Infrastructure.Db.Repositories;

public class UnitOfWork<TContext>(TContext context) : IUnitOfWork where TContext : DbContext
{
    public Task CommitAsync(CancellationToken cancelToken = default)
    {
        return context.SaveChangesAsync(cancelToken);
    }
}
