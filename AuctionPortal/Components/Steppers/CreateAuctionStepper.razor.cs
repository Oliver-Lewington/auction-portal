using AuctionPortal.Components.Steppers.Validation;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Steppers;

public partial class CreateAuctionStepper : StepperComponentBase<AuctionViewModel>
{
    [Inject] IAuctionService AuctionService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        InitializeViewModel(() => new AuctionViewModel
        {
            CreatorId = UserId
        });

        AddValidation(ValidationRules.GetAuctionValidationRules());
    }

    protected async Task OnCompletedChanged(bool completed)
    {
        if (!completed)
            return;

        try
        {
            var result = await AuctionService.CreateAuctionAsync(ViewModel);

            Snackbar.Add($"Auction '{result.Name}' created successfully!", Severity.Success);
            NavigationManager.NavigateTo($"/{result.Id}");
        }
        catch (Exception ex)
        {
            await HandleSubmitErrorAsync(ex, "creating auction");
        }
    }
}
