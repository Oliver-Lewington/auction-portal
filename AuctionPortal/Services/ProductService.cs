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

    public async Task<ProductViewModel> AddProductAsync(ProductViewModel productViewModel, CancellationToken cancellationToken = default)
    {
        if (productViewModel.Id == Guid.Empty)
            productViewModel.Id = Guid.NewGuid();

        var auction = await _dbContext.Auctions
            .Include(a => a.Products)
            .FirstOrDefaultAsync(a => a.Id == productViewModel.AuctionId, cancellationToken)
            .ConfigureAwait(false);

        if (auction == null)
            throw new InvalidOperationException($"Auction with Id '{productViewModel.AuctionId}' not found.");

        var productModel = _mapper.Map<ProductModel>(productViewModel);
        productModel.AuctionId = auction.Id;

        // Calculate sequence
        productModel.SaleSequence = CalculateNextSequence(auction.Products);

        // Calculate SaleEnd backwards from auction end
        var offsetMinutes = productModel.SaleSequence == 0
            ? 1
            : productModel.SaleSequence * auction.ProductSaleTimeInterval;

        productModel.SaleEnd = auction.EndsAt.AddMinutes(-offsetMinutes);

        productModel.CreatedAt = DateTime.UtcNow;
        productModel.UpdatedAt = DateTime.UtcNow;

        _dbContext.Products.Add(productModel);

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return _mapper.Map<ProductViewModel>(productModel);
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var productModels = await _dbContext.Products
            .Include(p => p.Images)
            .Include(p => p.Auction)
            .AsNoTracking()
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<IEnumerable<ProductViewModel>>(productModels);
    }

    public async Task<ProductViewModel> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var productModel = await _dbContext.Products
            .Include(p => p.Images)
            .Include(p => p.Auction)
            .Include(p => p.Bids)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            .ConfigureAwait(false);

        if (productModel == null)
            throw new InvalidOperationException($"Product with Id '{id}' not found.");

        return _mapper.Map<ProductViewModel>(productModel);
    }

    public async Task<ProductViewModel> UpdateProductAsync(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default)
    {
        var productModel = await _dbContext.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            .ConfigureAwait(false);

        if (productModel == null)
            throw new InvalidOperationException($"Product with Id '{id}' not found.");

        UpdateBasicFields(productModel, productViewModel);
        UpdateImages(productModel, productViewModel);

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return _mapper.Map<ProductViewModel>(productModel);
    }

    public async Task DeleteEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var productModel = await _dbContext.Products.FindAsync([id], cancellationToken);

        if (productModel != null)
        {
            _dbContext.Products.Remove(productModel);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private static int CalculateNextSequence(IEnumerable<ProductModel> products)
    {
        if (!products.Any())
            return 0;

        return products.Max(p => p.SaleSequence) + 1;
    }

    private static void UpdateBasicFields(ProductModel model, ProductViewModel vm)
    {
        model.Title = vm.Title;
        model.Description = vm.Description;
        model.StartingPrice = vm.StartingPrice;
        model.ReservePrice = vm.ReservePrice;
        model.UpdatedAt = DateTime.UtcNow;
    }

    private void UpdateImages(ProductModel model, ProductViewModel vm)
    {
        // Remove deleted images
        var vmImageIds = vm.Images.Select(i => i.Id).ToHashSet();

        var toRemove = model.Images
            .Where(i => !vmImageIds.Contains(i.Id))
            .ToList();

        foreach (var img in toRemove)
            _dbContext.ProductImages.Remove(img);

        // Update or add images
        foreach (var vmImg in vm.Images)
        {
            var existing = model.Images.FirstOrDefault(i => i.Id == vmImg.Id);

            if (existing != null)
            {
                existing.Url = vmImg.Url;
                existing.Alt = vmImg.Alt;
            }
            else
            {
                model.Images.Add(new ProductImageModel
                {
                    Url = vmImg.Url,
                    Alt = vmImg.Alt
                });
            }
        }
    }
}
