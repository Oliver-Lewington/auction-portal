using AutoMapper;
using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;

namespace AuctionPortal.Mapping;

public class AuctionMappingProfile : Profile
{
    public AuctionMappingProfile()
    {
        CreateMap<AuctionViewModel, AuctionModel>()
                   .ForMember(dest => dest.Creator, opt => opt.Ignore()) // handled manually
                   .ForMember(dest => dest.Id, opt => opt.Ignore())      // EF generates ID
                   .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                   .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<AuctionModel, AuctionViewModel>()
            .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.Creator.Id));
    }
}
