using AuctionPortal.Services;
using MudBlazor;

namespace AuctionPortal.Dialogs;

public static class ConfirmAndDeleteDialog
{
    public static async Task<bool> Generate(
        Guid id,
        IDataService dataService,
        IDialogService dialogService,
        ISnackbar snackbar,
        Func<Task>? refreshCallback = null)
    {
        if (id == Guid.Empty)
            return false;

        var parameters = new DialogParameters
        {
            { "ContentText", $"Are you sure you want to delete this entity? This cannot be undone." },
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
            await dataService.DeleteEntityByIdAsync(id);
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
