using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionPortal.Components.Pages.Product;

public partial class ViewAuctionProducts : ComponentBase
{
    [Parameter] public IReadOnlyList<ProductViewModel> ProductRecords { get; set; } = new List<ProductViewModel>();

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
}
