using AutoMapper;
using AuctionPortal.Data.Models;
using AuctionPortal.DTOs;
using AuctionPortal.ViewModels;

namespace AuctionPortal.Mapping;

public class AuctionMappingProfile : Profile
{
    public AuctionMappingProfile()
    {
        // EF Models -> DTOs
        CreateMap<AuctionModel, AuctionDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<ProductModel, ProductDto>()
            .ForMember(dest => dest.Sold, opt => opt.MapFrom(src => src.FinalBid.HasValue))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        CreateMap<ProductImageModel, ImageInfoDTO>();

        // DTOs -> ViewModels
        CreateMap<AuctionDto, AuctionViewModel>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<ProductDto, ProductViewModel>()
            .ForMember(dest => dest.Sold, opt => opt.MapFrom(src => src.Sold))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        CreateMap<ImageInfoDTO, ImageInfoViewModel>();
    }
}
