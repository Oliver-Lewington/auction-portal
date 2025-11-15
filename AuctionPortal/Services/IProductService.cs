using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;

namespace AuctionPortal.Services
{
    public interface IProductService : IDataService
    {
        Task<ProductViewModel> AddProductAsync(ProductViewModel productViewModel, CancellationToken cancellationToken = default);
        Task<ProductViewModel> UpdateProductAsync(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductViewModel>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<ProductViewModel> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
