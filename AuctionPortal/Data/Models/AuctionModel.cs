namespace AuctionPortal.Data.Models;

public class AuctionModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ApplicationUser Creator { get; set; } = null!;
    public DateTime BeginsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public bool LiveFlag { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<ProductModel> Products { get; set; } = new();
}
