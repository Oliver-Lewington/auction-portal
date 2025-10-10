using AuctionPortal.Data;
using AuctionPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Services;

public interface IAuctionService
{
    Task<Auction> CreateAuctionAsync(Auction auction, CancellationToken cancellationToken = default);
    Task<IEnumerable<Auction>> GetAuctionsAsync(CancellationToken cancellationToken = default);
    Task<Auction?> GetAuctionByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
