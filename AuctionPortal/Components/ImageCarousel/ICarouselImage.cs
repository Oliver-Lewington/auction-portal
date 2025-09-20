namespace AuctionPortal.Components.ImageCarousel;

public interface ICarouselImage
{
    /// <summary>
    /// Unique identifier for the image
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Image URL or path
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Optional alt text for accessibility
    /// </summary>
    public string Alt { get; }

    /// <summary>
    /// Optional caption or title
    /// </summary>
    public string Caption { get; }
}

