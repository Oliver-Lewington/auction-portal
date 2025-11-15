using AuctionPortal.Components.Steppers.Validation;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Steppers;

public partial class AuctionEditorStepper : StepperComponentBase<AuctionViewModel>
{
    [Parameter] public Guid? AuctionId { get; set; }   // Optional — null means "create mode"

    [Inject] IAuctionService AuctionService { get; set; } = default!;

    protected override bool IsEditMode => AuctionId.HasValue && AuctionId != Guid.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            if (!IsEditMode)
            {
                // Create mode: initialise empty model
                InitializeViewModel(() => new AuctionViewModel
                {
                    CreatorId = UserId
                });
            }
            else
            {
                // Edit mode: load existing auction
                var existingAuction = await AuctionService.GetAuctionByIdAsync(AuctionId.Value);

                if (existingAuction is null)
                {
                    Snackbar.Add("The specified auction could not be found.", Severity.Error);
                    NavigationManager.NavigateTo("/");
                    return;
                }

                InitializeViewModel(() => existingAuction);
            }
            AddValidation(ValidationRules.GetAuctionValidationRules());
        }
        catch (Exception ex)
        {
            Snackbar.Add("An error occurred while loading the auction data.", Severity.Error);
            NavigationManager.NavigateTo("/");
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected async Task OnCompletedChanged(bool completed)
    {
        if (!completed)
            return;

        try
        {
            AuctionViewModel result;

            if (!IsEditMode)
            {
                result = await AuctionService.CreateAuctionAsync(ViewModel);
                Snackbar.Add($"Auction '{result.Name}' created successfully!", Severity.Success);
            }
            else
            {
                result = await AuctionService.UpdateAuctionAsync(ViewModel);
                Snackbar.Add($"Auction '{result.Name}' updated successfully!", Severity.Success);
            }

            NavigationManager.NavigateTo($"auctions/{result.Id}");
        }
        catch (Exception ex)
        {
            await HandleSubmitErrorAsync(ex, IsEditMode ? "updating auction" : "creating auction");
        }
    }
}
