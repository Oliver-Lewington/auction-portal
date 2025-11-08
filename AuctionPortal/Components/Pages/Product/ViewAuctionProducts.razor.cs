using AuctionPortal.Dialogs;
using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionPortal.Components.Pages.Product;

public partial class ViewAuctionProducts : ComponentBase
{
    [Parameter] public ICollection<ProductViewModel> ProductRecords { get; set; } = new List<ProductViewModel>();

    [Inject] IProductService ProductService { get; set; } = default!;
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager Navigation { get; set; } = default!;

    private int currentPage = 1;
    private int pageSize = 4;

    private IEnumerable<ProductViewModel> CurrentPageProducts =>
        ProductRecords
            .OrderByDescending(p => p.Title)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);

    private int TotalPages => (int)Math.Ceiling(ProductRecords.Count / (double)pageSize);

    private void OnSelectedChanged(int page)
    {
        currentPage = page;
    }

    private void EditProduct(Guid id)
    {

    }

    private async Task DeleteProduct(ProductViewModel productViewModel) => await ConfirmAndDeleteDialog.Generate(
            productViewModel.Id,
            ProductService,
            DialogService,
            Snackbar,
            () => Task.Run(() => ProductRecords.Remove(productViewModel)));
}
