
using AuctionPortal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Data;
public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

    public DbSet<BidModel> Bids { get; set; }
    public DbSet<AuctionModel> Auctions { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<ProductImageModel> ProductImages { get; set; }
}
