using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore.NamingConventions.Internal;

namespace TaskTracker.Infrastructure.Db.Extensions;

public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// Generate singular table name using snake case naming convention.
    /// For instance, "TaskItem" -> "task_item".
    /// </summary>
    public static EntityTypeBuilder<TEntity> HasTypeTableName<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        string schema)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(entityTypeBuilder);
        ArgumentNullException.ThrowIfNull(schema);

        var nameRewriter = new SnakeCaseNameRewriter(CultureInfo.InvariantCulture);
        var tableName = nameRewriter.RewriteName(entityTypeBuilder.Metadata.ClrType.Name);
        return entityTypeBuilder.ToTable(tableName, schema);
    }
}
