using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.Data.Models;

public class ProductModel
{
    public Guid Id { get; set; }

    public Guid AuctionId { get; set; }
    public AuctionModel Auction { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal? ReservePrice { get; set; }

    public DateTime? ExpiryDate { get; set; }

    // Only final sale information
    public decimal? FinalBid { get; set; } // Null if unsold
    public string? FinalBidderName { get; set; } // Null if unsold
    public bool Sold => FinalBid.HasValue; // Computed property

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
