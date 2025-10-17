using AuctionPortal.Data.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace AuctionPortal.Components.Pages.Auction.Components;

public partial class AuctionSettings : ComponentBase
{
    [Inject] NavigationManager Navigation { get; set; } = default!;

    [Parameter] public AuctionModel? Auction { get; set; } // Check for nullability and throw error if null

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
        if (Auction is null)
            return; // Snackbar "Auction has not been passed successfully"

        Navigation.NavigateTo($"{Auction.Id}/products/create");
    }

    private void SaveSettings()
    {
        // Logic to save auction settings
    }
}
