using AuctionPortal.Components.ImageCarousel;

namespace AuctionPortal.Data.Models;

public class AuctionItemImage : ICarouselImage
{
    public AuctionItemImage(string url)
    {
        Url = url;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = null!;
    public string? Alt { get; set; }
    public string? Caption { get; set; }
}
