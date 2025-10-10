namespace AuctionPortal.Services;

public static class RouteTitleService
{
    private static readonly Dictionary<string, string> Titles = new()
    {
        ["/"] = "Home",
        ["/auctions"] = "Auctions",
        ["/auctions/create"] = "Create Auction",
        ["/products"] = "Products",
        ["/settings"] = "Settings"
    };

    public static string? GetTitle(string route)
    {
        Titles.TryGetValue(route, out var title);
        return title;
    }
}
