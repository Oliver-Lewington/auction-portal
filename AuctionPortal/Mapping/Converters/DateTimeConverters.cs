namespace AuctionPortal.Mapping.Converters;

using AutoMapper;

public class DateTimeToUtcConverter : ITypeConverter<DateTime, DateTime>
{
    public DateTime Convert(DateTime source, DateTime destination, ResolutionContext context)
        => source.Kind == DateTimeKind.Utc ? source : source.ToUniversalTime();
}

public class NullableDateTimeToUtcConverter : ITypeConverter<DateTime?, DateTime?>
{
    public DateTime? Convert(DateTime? source, DateTime? destination, ResolutionContext context)
        => source.HasValue
            ? source.Value.Kind == DateTimeKind.Utc ? source.Value : source.Value.ToUniversalTime()
            : null;
}
