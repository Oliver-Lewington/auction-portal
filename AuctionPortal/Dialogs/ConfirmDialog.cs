using MudBlazor;

namespace AuctionPortal.Dialogs;

public static class ConfirmDialog
{
    public static async Task<bool> Generate(ISnackbar snackbar, IDialogService dialogService, Func<Task>? callback = null)
    {
        var parameters = new DialogParameters<DeleteDialog>
        {
            { dl => dl.ContentText, "Do you really want to delete this image? This process cannot be undone." },
            { dl => dl.ButtonText, "Delete" },
            { dl => dl.Color, Color.Error }
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };
        var dialog = await dialogService.ShowAsync<DeleteDialog>("Confirm Delete", parameters, options);
        var result = await dialog.Result;

        if (result is not null && result.Canceled)
            return result.Canceled;

        try
        {
            if (callback != null)
                await callback();

            snackbar.Add("Image deleted successfully.", Severity.Success);

            return true;
        }
        catch (Exception ex)
        {
            snackbar.Add($"Failed to delete auction: {ex.Message}", Severity.Error);
            return false;
        }
    }
}
