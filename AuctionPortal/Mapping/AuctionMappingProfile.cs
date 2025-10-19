using AutoMapper;
using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;

namespace AuctionPortal.Mapping;

public class AuctionMappingProfile : Profile
{
    public AuctionMappingProfile()
    {
        // Data Models ↔ ViewModels
        CreateMap<AuctionViewModel, AuctionModel>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
            .ReverseMap();

        CreateMap<ProductModel, ProductViewModel>()
            .ForMember(dest => dest.Sold, opt => opt.MapFrom(src => src.FinalBid.HasValue))
            .ReverseMap()
            .ForMember(dest => dest.FinalBid, opt => opt.Ignore());

        CreateMap<ProductImageModel, ImageInfoViewModel>().ReverseMap();
    }
}
