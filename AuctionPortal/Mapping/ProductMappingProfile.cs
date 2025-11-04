using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;
using AutoMapper;

namespace AuctionPortal.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductModel, ProductViewModel>()
                .ForMember(dest => dest.Sold, opt => opt.MapFrom(src => src.FinalBid.HasValue))
                .ReverseMap()
                .ForMember(dest => dest.FinalBid, opt => opt.Ignore());


            CreateMap<ImageModel, ImageViewModel>().ReverseMap();
        }
    }
}
