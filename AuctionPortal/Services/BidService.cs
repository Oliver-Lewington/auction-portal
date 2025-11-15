using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using AuctionPortal.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Services;

using AuctionPortal.Hubs;
using Microsoft.AspNetCore.SignalR;

public class BidService : IBidService
{
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IHubContext<AuctionHub> _hubContext;

    public BidService(
        AuctionDbContext dbContext,
        IMapper mapper,
        IHubContext<AuctionHub> hubContext)
    { 
        _dbContext = dbContext;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public async Task<BidViewModel> CreateBidAsync(BidViewModel viewModel)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == viewModel.ProductId);

        if (product == null)
            throw new InvalidOperationException("Product not found.");

        var bidEntity = _mapper.Map<BidModel>(viewModel);
        if (bidEntity.Timestamp == default)
            bidEntity.Timestamp = DateTime.UtcNow;

        _dbContext.Bids.Add(bidEntity);

        product.FinalBid = bidEntity.Amount;
        product.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        var response = _mapper.Map<BidViewModel>(bidEntity);

        // 🔥 Broadcast the bid to all clients in the product group
        await _hubContext.Clients
            .Group(product.Id.ToString())
            .SendAsync("BidUpdated", response);

        return response;
    }

    public async Task<IEnumerable<BidViewModel>> GetBidsByProductIdAsync(Guid productId)
    {
        var bids = await _dbContext.Bids
            .Where(b => b.ProductId == productId)
            .OrderByDescending(b => b.Timestamp)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<BidViewModel>>(bids);
    }

    public async Task<decimal> GetCurrentBidAmount(Guid productId)
    {
        // Get the latest bid for the product
        decimal lastBid = await _dbContext.Bids
            .Where(b => b.ProductId == productId)
            .OrderByDescending(b => b.Timestamp)
            .Select(b => b.Amount) 
            .FirstOrDefaultAsync(); 

        // If there’s a bid, use it; otherwise fallback to the product’s starting price
        if (lastBid > 0)
            return lastBid;

        var product = await _dbContext.Products
            .Where(p => p.Id == productId)
            .Select(p => p.StartingPrice)
            .FirstOrDefaultAsync();

        return product; // product.StartingPrice is decimal
    }

}
