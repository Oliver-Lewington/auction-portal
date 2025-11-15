using AuctionPortal.ViewModels;

namespace AuctionPortal.Services;

public interface IBidService
{
    Task<BidViewModel> CreateBidAsync(BidViewModel viewModel);
    Task<IEnumerable<BidViewModel>> GetBidsByProductIdAsync(Guid productId);
    Task<decimal> GetCurrentBidAmount(Guid productId);
}

