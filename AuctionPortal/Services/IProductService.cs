using AuctionPortal.Data.Models;

namespace AuctionPortal.Services
{
    public interface IProductService
    {
        Task<Product> AddAuctionProductAsync(Product item, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAuctionProductsAsync(CancellationToken cancellationToken = default);
        Task<Product?> GetAuctionProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
