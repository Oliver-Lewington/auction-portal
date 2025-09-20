
using AuctionPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Data;
public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

    public DbSet<Bid> Bids { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<AuctionItem> AuctionItems { get; set; }
}
