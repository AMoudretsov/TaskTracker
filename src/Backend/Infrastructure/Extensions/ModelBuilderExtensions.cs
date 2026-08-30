using Microsoft.EntityFrameworkCore;
using TaskTracker.Infrastructure.Common;

namespace TaskTracker.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder AddCaseInsensitiveCollation(this ModelBuilder modelBuilder)
    {
        return modelBuilder.HasCollation(
            name: Collations.TasksCaseInsensitive,
            locale: "und-u-kf-upper-ks-level2",
            provider: "icu",
            deterministic: false);
    }
}
