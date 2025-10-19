using AuctionPortal.ViewModels;

namespace AuctionPortal.Services;

public interface IAuctionService
{
    Task<AuctionViewModel> CreateAuctionAsync(AuctionViewModel auctionViewModel, CancellationToken cancellationToken = default);
    Task UpdateAuctionAsync(AuctionViewModel auctionViewModel, CancellationToken cancellationToken = default);
    Task<IEnumerable<AuctionViewModel>> GetAuctionsAsync(CancellationToken cancellationToken = default);
    Task<AuctionViewModel> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAuctionAsync(Guid id, CancellationToken cancellationToken = default);
}
