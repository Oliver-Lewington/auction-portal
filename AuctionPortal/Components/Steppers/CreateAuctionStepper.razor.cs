using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Steppers;

public partial class CreateAuctionStepper : ComponentBase
{
    [Inject] IAuctionService AuctionService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    private MudStepper stepper = default!;
    private AuctionViewModel auctionViewModel = new();
    private StepperValidator<AuctionViewModel> validator = default!;

    protected override void OnInitialized()
    {
        validator = new StepperValidator<AuctionViewModel>(auctionViewModel);

        // STEP 1
        validator.AddRule(0, a => !string.IsNullOrWhiteSpace(a.Name), "Name is required.");

        // STEP 2
        validator.AddRule(1, a => a.BeginsAt.Date >= DateTime.Today.AddDays(1),
            "Auction must start on or after tomorrow.");
        validator.AddRule(1, a => a.EndsAt > a.BeginsAt,
            "End time must be after the start time.");
    }

    private async Task OnSubmitAuction()
    {
        try
        {
            var savedAuction = await AuctionService.CreateAuctionAsync(auctionViewModel);

            Snackbar.Add($"Auction '{savedAuction.Name}' created successfully!", Severity.Success);
            NavigationManager.NavigateTo($"/{savedAuction.Id}");
        }
        catch (Exception ex)
        {
            var activeStep = stepper.ActiveStep;

            if (activeStep != null)
            {
                await activeStep.SetHasErrorAsync(true, true);
                await activeStep.SetCompletedAsync(false, true);
            }

            Snackbar.Add($"Failed to create auction: {ex.Message}", Severity.Error);
        }
    }
}