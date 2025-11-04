using AuctionPortal.Components.ImageCarousel;

namespace AuctionPortal.ViewModels;

public class ImageViewModel : ICarouselImage
{
    public ImageViewModel(string url)
    {
        Url = url;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = string.Empty;
    public string? Alt { get; set; }
    public string? Caption { get; set; }
}
