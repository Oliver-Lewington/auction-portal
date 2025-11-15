using MudBlazor;

namespace AuctionPortal.Services;

public class BreadcrumbService
{
    public event Action? OnChanged;

    public List<BreadcrumbItem> Items { get; private set; } = new();

    public void SetBreadcrumbs(IEnumerable<BreadcrumbItem> items)
    {
        Items = items.ToList();
        OnChanged?.Invoke();
    }

    public void Clear()
    {
        Items.Clear();
        OnChanged?.Invoke();
    }
}
