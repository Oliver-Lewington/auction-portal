namespace AuctionPortal.Data.Models;

public class AuctionItem
{
    public Guid Id { get; set; }
    public Auction Auction { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal StartingPrice { get; set; }
    public decimal ReservePrice { get; set; }

    // Only final sale information
    public decimal? FinalBid { get; set; } // Null if unsold
    public string? FinalBidderName { get; set; } // Null if unsold
    public bool Sold => FinalBid.HasValue; // Computed property

    public List<AuctionItemImage> Images { get; set; } = new();  
    public string Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
