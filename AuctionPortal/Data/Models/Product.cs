using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.Data.Models;

public class Product
{
    public Product(Guid auctionId)
    {
        Id = Guid.NewGuid();
        AuctionId = auctionId;
    }

    public Guid Id { get; set; }

    [ForeignKey("Auction")]
    public Guid AuctionId { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal? ReservePrice { get; set; }

    public DateTime? ExpiryDate { get; set; }

    // Only final sale information
    public decimal? FinalBid { get; set; } // Null if unsold
    public string? FinalBidderName { get; set; } // Null if unsold
    public bool Sold => FinalBid.HasValue; // Computed property

    public List<ProductImage> Images { get; set; } = new();  
    public string Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
