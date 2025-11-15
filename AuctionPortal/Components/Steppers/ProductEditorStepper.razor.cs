using AuctionPortal.Components.Steppers.Validation;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Steppers;

public partial class ProductEditorStepper : StepperComponentBase<ProductViewModel>
{
    [Parameter] public Guid AuctionId { get; set; }
    [Parameter] public Guid? ProductId { get; set; } // Optional — null means create mode

    [Inject] IProductService ProductService { get; set; } = default!;

    protected override bool IsEditMode => ProductId.HasValue && ProductId != Guid.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        IsLoading = true;

        try
        {
            if (!IsEditMode)
            {
                // Create mode: initialise empty model
                InitializeViewModel(() => new ProductViewModel(AuctionId));
            }
            else
            {
                // Edit mode: load existing product
                var existingProduct = await ProductService.GetProductByIdAsync(ProductId.Value);

                if (existingProduct == null)
                {
                    Snackbar.Add("The specified product could not be found.", Severity.Error);
                    NavigationManager.NavigateTo($"/auctions/{AuctionId}");
                    return;
                }

                InitializeViewModel(() => existingProduct);
            }

            AddValidation(ValidationRules.GetProductValidationRules());
        }
        catch (Exception ex)
        {
            Snackbar.Add("An error occurred while loading the product data.", Severity.Error);
            NavigationManager.NavigateTo($"/auctions/{AuctionId}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected async Task OnCompletedChanged(bool completed)
    {
        if (!completed) return;

        try
        {
            ProductViewModel result;

            if (!IsEditMode)
            {
                result = await ProductService.AddProductAsync(ViewModel);
                Snackbar.Add($"Auction item '{result.Title}' created successfully!", Severity.Success);
            }
            else
            {
                result = await ProductService.UpdateProductAsync(ViewModel.Id, ViewModel);
                Snackbar.Add($"Auction item '{result.Title}' updated successfully!", Severity.Success);
            }

            NavigationManager.NavigateTo($"/auctions/{AuctionId}/products/{result.Id}");
        }
        catch (Exception ex)
        {
            await HandleSubmitErrorAsync(ex, IsEditMode ? "updating product" : "creating product");
        }
    }
}
