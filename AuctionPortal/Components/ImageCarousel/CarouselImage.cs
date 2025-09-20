namespace AuctionPortal.Components.ImageCarousel;
public class CarouselImage : ICarouselImage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = null!;
    public string Alt { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
}
