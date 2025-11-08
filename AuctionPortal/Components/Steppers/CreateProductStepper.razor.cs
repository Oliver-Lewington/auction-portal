using AuctionPortal.Components.Steppers.Validation;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Steppers;

public partial class CreateProductStepper : StepperComponentBase<ProductViewModel>
{
    [Parameter] public Guid AuctionId { get; set; }

    [Inject] IProductService ProductService { get; set; } = default!;

    private DateTime? ExpiryDateNullable
    {
        get => ViewModel.ExpiryDate != default ? ViewModel.ExpiryDate : null;
        set => ViewModel.ExpiryDate = value ?? default;
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        InitializeViewModel(() => new ProductViewModel(AuctionId));

        AddValidation(ValidationRules.GetProductValidationRules());
    }

    protected async Task OnCompletedChanged(bool completed)
    {
        if (!completed)
            return;

        try
        {
            var result = await ProductService.AddAuctionProductAsync(ViewModel);

            Snackbar.Add($"Auction item '{result.Title}' saved successfully!", Severity.Success);
            NavigationManager.NavigateTo($"/{AuctionId}");
        }
        catch (Exception ex)
        {
            await HandleSubmitErrorAsync(ex, "creating product");
        }
    }
}
