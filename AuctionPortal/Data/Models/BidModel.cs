namespace AuctionPortal.Data.Models;

public class BidModel
{
    public Guid Id { get; set; }
    public ProductModel AuctionItem { get; set; } = null!;

    public UserModel? User { get; set; }

    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

