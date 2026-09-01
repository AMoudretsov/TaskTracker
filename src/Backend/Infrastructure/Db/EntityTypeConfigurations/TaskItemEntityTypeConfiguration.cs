using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Core.Entities;
using TaskTracker.Infrastructure.Db.Common;
using TaskTracker.Infrastructure.Db.Extensions;

namespace TaskTracker.Infrastructure.Db.EntityTypeConfigurations;

public class TaskItemEntityTypeConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder
            .HasTypeTableName(Schemas.Tasks)
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Title)
            .HasMaxLength(128)
            .UseCaseInsensitiveCollation();

        builder
            .Property(t => t.Description)
            .HasMaxLength(4096)
            .UseCaseInsensitiveCollation();

        builder
            .Property(t => t.IsCompleted)
            .HasDefaultValue(false);

        builder
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql(Functions.Now);

        builder
            .HasIndex(t => t.Title)
            .IsUnique();
    }
}
