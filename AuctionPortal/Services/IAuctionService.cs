using AuctionPortal.Data.Models;

namespace AuctionPortal.Services;

public interface IAuctionService
{
    Task<AuctionModel> CreateAuctionAsync(AuctionModel auction, CancellationToken cancellationToken = default);
    Task<IEnumerable<AuctionModel>> GetAuctionsAsync(CancellationToken cancellationToken = default);
    Task<AuctionModel?> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAuctionAsync(AuctionModel auction);
    Task DeleteAuctionAsync(Guid id);
}
