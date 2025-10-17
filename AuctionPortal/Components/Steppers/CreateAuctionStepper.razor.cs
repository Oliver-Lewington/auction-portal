using AuctionPortal.Data.Models;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Steppers;

public partial class CreateAuctionStepper : ComponentBase
{
    [Inject] IAuctionService AuctionService { get; set; } = default!;
    [Inject] IMapper Mapper { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;


    private AuctionViewModel auction = new();
    private StepperValidator<AuctionViewModel> validator = default!;
    private MudStepper stepper = default!;

    protected override void OnInitialized()
    {
        validator = new StepperValidator<AuctionViewModel>(auction);

        // STEP 1
        validator.AddRule(0, a => !string.IsNullOrWhiteSpace(a.Name), "Name is required.");

        // STEP 2
        validator.AddRule(1, a => a.StartDateTime.Date >= DateTime.Today.AddDays(1),
            "Auction must start on or after tomorrow.");
        validator.AddRule(1, a => a.EndDateTime > a.StartDateTime,
            "End time must be after the start time.");
    }

    private async Task OnSubmitAuction()
    {
        try
        {
            var model = new AuctionModel
            {
                Name = auction.Name,
                Description = auction.Description,
                StartTime = auction.StartDateTime,
                EndTime = auction.EndDateTime,
                LiveFlag = auction.LiveFlag
            };

            var savedAuction = await AuctionService.CreateAuctionAsync(model);

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