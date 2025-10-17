using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Services;

public class ProductService : IProductService
{
    private readonly AuctionDbContext _dbContext;

    public ProductService(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductModel> AddAuctionProductAsync(ProductModel item, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Add(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<IEnumerable<ProductModel>> GetAuctionProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ProductModel?> GetAuctionProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task<ProductModel?> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
