using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AuctionPortal.Services;

public class AuctionService : IAuctionService
{
    private readonly AuctionDbContext _dbContext;

    public AuctionService(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuctionModel> CreateAuctionAsync(AuctionModel auction, CancellationToken cancellationToken = default)
    {
        if (auction.Id == Guid.Empty)
            auction.Id = Guid.NewGuid();

        _dbContext.Auctions.Add(auction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return auction;
    }

    public async Task<IEnumerable<AuctionModel>> GetAuctionsAsync(CancellationToken cancellationToken = default) => 
        await _dbContext.Auctions
                .Include(a => a.Products)
                .AsNoTracking().OrderBy(a => a.EndTime)
                .ToListAsync(cancellationToken);

    public async Task<AuctionModel?> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
         await _dbContext.Auctions
                .Include(a => a.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public Task UpdateAuctionAsync(AuctionModel auction)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAuctionAsync(Guid id)
    {
        var auction = await _dbContext.Auctions.FindAsync(id);
        if (auction != null)
        {
            _dbContext.Auctions.Remove(auction);
            await _dbContext.SaveChangesAsync();
        }
    }


}
