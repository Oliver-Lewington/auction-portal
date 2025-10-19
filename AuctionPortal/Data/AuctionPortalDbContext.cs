
using AuctionPortal.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionPortal.Data;

public class AuctionDbContext : IdentityDbContext<ApplicationUser>
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

    public DbSet<AuctionModel> Auctions { get; set; }
    public DbSet<ProductModel> Products { get; set; }
}
