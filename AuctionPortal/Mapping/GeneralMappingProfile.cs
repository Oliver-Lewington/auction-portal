using AuctionPortal.Mapping.Converters;
using AutoMapper;

namespace AuctionPortal.Mapping;

public class GeneralMappingProfile : Profile
{
    public GeneralMappingProfile()
    {
        // Apply to all DateTime and DateTime? mappings (For PostGreSql connection)
        CreateMap<DateTime, DateTime>().ConvertUsing<DateTimeToUtcConverter>();
        CreateMap<DateTime?, DateTime?>().ConvertUsing<NullableDateTimeToUtcConverter>();
    }
}
