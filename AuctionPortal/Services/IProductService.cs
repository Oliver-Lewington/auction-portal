using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;

namespace AuctionPortal.Services
{
    public interface IProductService
    {
        Task<ProductViewModel> AddAuctionProductAsync(ProductViewModel item, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductViewModel>> GetAuctionProductsAsync(CancellationToken cancellationToken = default);
        Task<ProductViewModel> GetAuctionProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
