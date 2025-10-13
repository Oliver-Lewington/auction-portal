using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Services;

public class AuctionService : IAuctionService
{
    private readonly AuctionDbContext _dbContext;

    public AuctionService(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Auction> CreateAuctionAsync(Auction auction, CancellationToken cancellationToken = default)
    {
        if (auction.Id == Guid.Empty)
            auction.Id = Guid.NewGuid();

        _dbContext.Auctions.Add(auction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return auction;
    }

    public async Task<IEnumerable<Auction>> GetAuctionsAsync(CancellationToken cancellationToken = default) => 
        await _dbContext.Auctions
                .Include(a => a.Products)
                .AsNoTracking().OrderBy(a => a.EndTime)
                .ToListAsync(cancellationToken);

    public async Task<Auction?> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
         await _dbContext.Auctions
                .Include(a => a.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public Task UpdateAuctionAsync(Auction auction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuctionAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
