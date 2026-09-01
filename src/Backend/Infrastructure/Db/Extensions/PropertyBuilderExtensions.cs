using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Infrastructure.Db.Common;

namespace TaskTracker.Infrastructure.Db.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> UseCaseInsensitiveCollation<TProperty>(
        this PropertyBuilder<TProperty> propertyBuilder)
    {
        ArgumentNullException.ThrowIfNull(propertyBuilder);

        return propertyBuilder.UseCollation(Collations.TasksCaseInsensitive);
    }
}
