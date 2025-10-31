using AuctionPortal.Components.Account;
using AuctionPortal.Components.ImageCarousel;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Steppers;

public partial class CreateProductStepper : ProtectedPageBase
{
    [Parameter] public Guid AuctionId { get; set; }

    [Inject] IProductService ProductService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    private ProductViewModel product = default!;
    private MudStepper stepper = default!;
    private List<ICarouselImage> carouselImages = new();
    private StepperValidator<ProductViewModel> validator = default!;

    private DateTime? ExpiryDateNullable
    {
        get => product.ExpiryDate != default ? product.ExpiryDate : null;
        set => product.ExpiryDate = value ?? default;
    }

    protected override void OnInitialized()
    {
        product = new ProductViewModel(AuctionId);
        validator = new StepperValidator<ProductViewModel>(product);

        // STEP 1: At least one image
        validator.AddRule(0, p => carouselImages != null && carouselImages.Any(), "Please upload at least one image.");

        // STEP 2: Product title required
        validator.AddRule(1, p => !string.IsNullOrWhiteSpace(p.Title), "Product name is required.");

        // STEP 3: Pricing and expiry
        validator.AddRule(2, p => p.StartingPrice > 0, "Starting price must be greater than 0.");
        validator.AddRule(2, p => p.ExpiryDate == default || p.ExpiryDate > DateTime.Now, "Expiry date must be in the future.");
    }

    private async Task OnSubmitProduct()
    {
        try
        {
            var savedProduct = await ProductService.AddAuctionProductAsync(product);

            NavigationManager.NavigateTo($"/{AuctionId}");
            Snackbar.Add($"Auction item '{savedProduct.Title}' saved successfully!", Severity.Success);
        }
        catch (Exception ex)
        {
            var activeStep = stepper.ActiveStep;

            if(activeStep != null)
            {
                await activeStep.SetHasErrorAsync(true, true);
                await activeStep.SetCompletedAsync(false, true);
            }

            Snackbar.Add($"Failed to save auction item: {ex.Message}", Severity.Error);
        }
    }
}