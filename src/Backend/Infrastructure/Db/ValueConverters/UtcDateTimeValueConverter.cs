using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TaskTracker.Infrastructure.Db.ValueConverters;

public class UtcDateTimeValueConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeValueConverter() : base(
        model => ConvertToUtc(model),
        provider => DateTime.SpecifyKind(provider, DateTimeKind.Utc))
    {
    }

    private static DateTime ConvertToUtc(DateTime value) =>
        value.Kind switch
        {
            DateTimeKind.Unspecified => DateTime.SpecifyKind(value, DateTimeKind.Utc),
            DateTimeKind.Local => value.ToUniversalTime(),
            DateTimeKind.Utc => value,
            _ => throw new NotSupportedException($"Unknown {nameof(DateTimeKind)}.{value.Kind}")
        };
}
