using AuctionPortal.Data.Models;

namespace AuctionPortal.Services;

public interface IAuctionService
{
    Task<Auction> CreateAuctionAsync(Auction auction, CancellationToken cancellationToken = default);
    Task<IEnumerable<Auction>> GetAuctionsAsync(CancellationToken cancellationToken = default);
    Task<Auction?> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAuctionAsync(Auction auction);
    Task DeleteAuctionAsync(Guid id);
}
