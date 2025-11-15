using AuctionPortal.Data.Models;
using AuctionPortal.Mapping.Converters;
using AuctionPortal.ViewModels;
using AutoMapper;

namespace AuctionPortal.Mapping;

public class AuctionMappingProfile : Profile
{
    public AuctionMappingProfile()
    {
        CreateMap<AuctionViewModel, AuctionModel>()
            .ForMember(dest => dest.Creator, opt => opt.Ignore()) // handled manually
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<AuctionModel, AuctionViewModel>()
            .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.Creator.Id));

        CreateMap<ImageViewModel, AuctionImageModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Added to enable new images to be added (view VM), without FK restraint.
            .ReverseMap();
    }
}
