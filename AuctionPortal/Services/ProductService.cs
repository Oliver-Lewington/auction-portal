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

    public async Task<Product> AddAuctionProductAsync(Product item, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Add(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<IEnumerable<Product>> GetAuctionProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetAuctionProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
