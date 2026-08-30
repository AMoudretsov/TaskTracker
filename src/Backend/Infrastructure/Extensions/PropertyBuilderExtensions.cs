using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Infrastructure.Common;

namespace TaskTracker.Infrastructure.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> UseCaseInsensitiveCollation<TProperty>(
        this PropertyBuilder<TProperty> propertyBuilder)
    {
        ArgumentNullException.ThrowIfNull(propertyBuilder);

        return propertyBuilder.UseCollation(Collations.TasksCaseInsensitive);
    }
}
