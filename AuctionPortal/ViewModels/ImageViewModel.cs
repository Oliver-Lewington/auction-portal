using AuctionPortal.Components.ImageViewer;
using Microsoft.AspNetCore.Components;

namespace AuctionPortal.ViewModels;

public class ImageViewModel : ICarouselImage
{
    public ImageViewModel() { }

    public ImageViewModel(string url, NavigationManager? navigationManager = null)
    {
        Url = url ?? string.Empty;
        Uri = BuildUri(url, navigationManager);
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = string.Empty;
    public string? Alt { get; set; }
    public string? Caption { get; set; }
    public Uri? Uri { get; set; }

    private static Uri? BuildUri(string? url, NavigationManager? navigationManager)
    {
        if (string.IsNullOrWhiteSpace(url))
            return null;

        if (Uri.TryCreate(url, UriKind.Absolute, out var absolute))
            return absolute;

        if (navigationManager != null)
            return new Uri(new Uri(navigationManager.BaseUri), url.TrimStart('/'));

        return new Uri(url, UriKind.Relative);
    }
}
