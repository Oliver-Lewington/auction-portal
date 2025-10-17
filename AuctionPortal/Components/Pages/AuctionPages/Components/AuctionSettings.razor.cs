using Microsoft.AspNetCore.Components;
using AuctionPortal.Data.Models;
using System.Diagnostics;

namespace AuctionPortal.Components.Pages.AuctionPages.Components;

public partial class AuctionSettings : ComponentBase
{
    [Inject] NavigationManager Navigation { get; set; } = null!;

    [Parameter] public Auction? Auction { get; set; } // Check for nullability and throw error if null

    private string _auctionStatus = "Live";
    private bool _notifyBidders = true;
    private bool _notifyWinners = true;
    private bool _notifyOutbid = false;

    protected override void OnParametersSet()
    {
        Debug.WriteLine($"AuctionSetting received Auction: {Auction?.Id}");
    }

    private void DeleteAuction()
    {
        // Navigation logic to add product page
    }

    private void EditAuction()
    {
        // Navigation logic to add product page
    }

    private void CreateProduct()
    {
        // Navigation logic to add product page
    }

    private void SaveSettings()
    {
        // Logic to save auction settings
    }
}
