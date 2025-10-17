namespace AuctionPortal.ViewModels;

public class ImageInfoViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = string.Empty;
    public string? Alt { get; set; }
    public string? Caption { get; set; }
}
