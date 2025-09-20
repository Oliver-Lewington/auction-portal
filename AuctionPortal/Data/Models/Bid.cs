namespace AuctionPortal.Data.Models;

public class Bid
{
    public Guid Id { get; set; }
    public AuctionItem AuctionItem { get; set; } = null!;

    public User? User { get; set; }

    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

