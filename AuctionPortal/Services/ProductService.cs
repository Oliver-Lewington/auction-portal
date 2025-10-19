using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Services;

public class ProductService : IProductService
{
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductService(AuctionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductViewModel> AddAuctionProductAsync(ProductViewModel productViewModel, CancellationToken cancellationToken = default)
    {
        if (productViewModel.Id == Guid.Empty)
            productViewModel.Id = Guid.NewGuid();

        var productModel = _mapper.Map<ProductViewModel, ProductModel>(productViewModel);

        _dbContext.Products.Add(productModel);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return productViewModel;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAuctionProductsAsync(CancellationToken cancellationToken = default)
    {
        var productModels = await _dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<ProductModel>, IEnumerable<ProductViewModel>>(productModels);
    }

    public async Task<ProductViewModel> GetAuctionProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var productModel = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (productModel == null)
            throw new Exception($"Product with Id {id} not found.");

        return _mapper.Map<ProductModel, ProductViewModel>(productModel);
    }

    public async Task UpdateProductAsync(ProductViewModel productViewModel, CancellationToken cancellationToken = default)
    {
        var productModel = await _dbContext.Products.FindAsync(productViewModel.Id);
        if (productModel == null)
            throw new Exception($"Product with Id {productViewModel.Id} not found.");

        _mapper.Map(productViewModel, productModel);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var productModel = await _dbContext.Products.FindAsync(id);
        if (productModel != null)
        {
            _dbContext.Products.Remove(productModel);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
