using AuctionPortal.Dialogs;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Pages.Auction.Components;

public partial class AuctionSettings : ComponentBase
{
    [Inject] IAuctionService AuctionService { get; set; } = default!;
    [Inject] NavigationManager Navigation { get; set; } = default!;
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    [Parameter] public AuctionViewModel Auction { get; set; } = default!;

    private string _auctionStatus = "Live";
    private bool _notifyBidders = true;
    private bool _notifyWinners = true;
    private bool _notifyOutbid = false;

    private async Task DeleteAuction()
    {
        await AuctionDialogs.ConfirmAndDeleteEntity(
            Auction.Id,
            AuctionService,
            DialogService,
            Snackbar,
            () => Task.Run(()=>Navigation.NavigateTo($"/")));

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
