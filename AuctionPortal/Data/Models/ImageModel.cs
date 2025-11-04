namespace AuctionPortal.Data.Models;

public class ImageModel
{
    public Guid Id { get; set; }
    public string Url { get; set; } = null!;
    public string? Alt { get; set; }
    public string? Caption { get; set; }
}
