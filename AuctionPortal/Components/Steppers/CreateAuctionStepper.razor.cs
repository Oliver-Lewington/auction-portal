using AuctionPortal.Components.Account;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;

namespace AuctionPortal.Components.Steppers;

public partial class CreateAuctionStepper : ProtectedPageBase
{
    [Inject] new ISnackbar Snackbar { get; set; } = default!;
    [Inject] IAuctionService AuctionService { get; set; } = default!;
    [Inject] protected new AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    private MudStepper stepper = default!;
    private AuctionViewModel auctionViewModel = new();
    private StepperValidator<AuctionViewModel> validator = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                // Get the user's database ID from their claims
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    auctionViewModel.CreatorId = userId;
                }
                else
                {
                    Console.Error.WriteLine("User ID claim not found.");
                }
            }
            else
            {
                //NavigationManager.NavigateTo("Account/Login", true);
            }

            validator = new StepperValidator<AuctionViewModel>(auctionViewModel);

            // STEP 1
            validator.AddRule(0, a => !string.IsNullOrWhiteSpace(a.Name), "Name is required.");

            // STEP 2
            validator.AddRule(1, a => a.BeginsAt.Date >= DateTime.Today.AddDays(1),
                "Auction must start on or after tomorrow.");
            validator.AddRule(1, a => a.EndsAt > a.BeginsAt,
                "End time must be after the start time.");
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine("User ID claim not found.");
        }

    }

    private async Task OnSaveDraftAndAddProducts()
    {
        try
        {
            // Mark the auction as a draft
            auctionViewModel.IsDraft = true;

            // Save draft
            var draftAuction = await AuctionService.CreateAuctionAsync(auctionViewModel);

            Snackbar.Add($"Draft auction '{draftAuction.Name}' saved.", Severity.Info);

            // Redirect to add products with return URL and auction ID
            var returnUrl = Uri.EscapeDataString(NavigationManager.Uri);
            NavigationManager.NavigateTo($"/product/create?auctionId={draftAuction.Id}&return={returnUrl}");
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Failed to save draft: {ex.Message}", Severity.Error);
        }
    }

    private async Task NavigateToAddProduct()
    {
        var redirectUrl = Uri.EscapeDataString(NavigationManager.Uri);
        await OnCreateAuction();
        NavigationManager.NavigateTo($"/product/create?return={redirectUrl}?");
    }

    private async Task OnCreateAuction()
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