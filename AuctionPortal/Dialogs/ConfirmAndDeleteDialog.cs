using AuctionPortal.Data.Models;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using MudBlazor;

namespace AuctionPortal.Dialogs;

public static class AuctionDialogs
{
    public static async Task<bool> ConfirmAndDeleteAuction(
        Guid auctionId,
        IAuctionService auctionService,
        IDialogService dialogService,
        ISnackbar snackbar,
        Func<Task>? refreshCallback = null)
    {
        if (auctionId == Guid.Empty)
            return false;

        var parameters = new DialogParameters
        {
            { "ContentText", $"Are you sure you want to delete this auction? This cannot be undone." },
            { "ButtonText", "Delete" },
            { "Color", Color.Error }
        };

        var options = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<DeleteDialog>("Confirm Delete", parameters, options);
        var result = await dialog.Result;

        if (result != null && result.Canceled)
            return false;

        try
        {
            await auctionService.DeleteAuctionAsync(auctionId);
            snackbar.Add("Auction deleted successfully.", Severity.Success);

            if (refreshCallback != null)
                await refreshCallback();

            return true;
        }
        catch (Exception ex)
        {
            snackbar.Add($"Failed to delete auction: {ex.Message}", Severity.Error);
            return false;
        }
    }
}
