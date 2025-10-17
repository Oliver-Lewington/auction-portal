using AuctionPortal.Data.Models;

namespace AuctionPortal.Services
{
    public interface IProductService
    {
        Task<ProductModel> AddAuctionProductAsync(ProductModel item, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductModel>> GetAuctionProductsAsync(CancellationToken cancellationToken = default);
        Task<ProductModel?> GetAuctionProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProductModel?> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
