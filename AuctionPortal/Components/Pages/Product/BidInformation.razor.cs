using AuctionPortal.Services;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Timer = System.Timers.Timer;

namespace AuctionPortal.Components.Pages.Product;

public partial class BidInformation : ComponentBase, IDisposable
{
    [Parameter] public bool EnableBidding { get; set; } = false;

    private ProductViewModel? _product = default!;
    [Parameter]
    public ProductViewModel? Product
    {
        get => _product;
        set
        {
            if (_product != value)
            {
                _product = value;
                OnProductChanged();
            }
        }
    }

    [Inject] IBidService BidService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] AuctionHubClientService HubClientService { get; set; } = default!;

    // Bid state
    private decimal CurrentBid { get; set; }
    private BidViewModel NewBid { get; set; } = new();
    private bool IsPlacingBid { get; set; } = false;
    private int BidderNumber;

    // Countdown state
    private CountdownViewModel Countdown { get; set; } = new();
    private Timer? CountdownTimer;

    private MudForm? BidFormRef;

    private IEnumerable<(string Label, int Value)> CountdownDisplay =>
        new[]
        {
            ("DAY".Pluralize(Countdown.Days), Countdown.Days),
            ("HOUR".Pluralize(Countdown.Hours), Countdown.Hours),
            ("MINUTE".Pluralize(Countdown.Minutes), Countdown.Minutes),
            ("SECOND".Pluralize(Countdown.Seconds), Countdown.Seconds)
        };

    protected override async Task OnInitializedAsync()
    {
        HubClientService.BidUpdated += OnBidUpdated;

        await HubClientService.StartAsync();

        // Join SignalR group ONLY here
        if (Product != null)
            await HubClientService.JoinProductGroup(Product.Id);
    }

    private async void OnProductChanged()
    {
        if (Product == null) return;

        InitializeCountdown();
        StartCountdownTimer();

        // Set current bid
        var lastBid = await BidService.GetCurrentBidAmount(Product.Id);
        CurrentBid = lastBid > 0 ? lastBid : Product.StartingPrice;

        // Reset bidder number
        BidderNumber = Product.Bids.Count + 1;

        // Prepare clean bid
        NewBid = new BidViewModel
        {
            BidderName = $"Bidder {BidderNumber}",
            Amount = null
        };

        // ❌ Removed second JoinProductGroup() to prevent double SignalR events

        StateHasChanged();
    }

    private async void OnBidUpdated(BidViewModel bid)
    {
        if (bid.ProductId != Product?.Id) return;

        // ✅ Prevent duplicate insertions
        if (!Product.Bids.Any(x => x.Id == bid.Id))
            Product.Bids.Insert(0, bid);

        CurrentBid = bid.Amount.GetValueOrDefault();

        Snackbar.Add(
            $"Bid of £{FormatAmount(CurrentBid)} placed successfully by {bid.BidderName}",
            Severity.Success);

        IsPlacingBid = false;

        await InvokeAsync(StateHasChanged);
    }

    private void IncreaseBid(int increment) =>
        NewBid.Amount = (int)CurrentBid + increment;

    private void InitializeCountdown() =>
        Countdown = new CountdownViewModel(Product.CreatedAt, Product.SaleEnd);

    private void StartCountdownTimer()
    {
        CountdownTimer?.Stop();
        CountdownTimer?.Dispose();

        CountdownTimer = new Timer(1000);
        CountdownTimer.Elapsed += async (_, __) =>
        {
            await InvokeAsync(() =>
            {
                InitializeCountdown();
                StateHasChanged();
            });
        };
        CountdownTimer.Start();
    }

    private async Task SubmitBid()
    {
        if (BidFormRef == null) return;

        await BidFormRef.Validate();
        if (!BidFormRef.IsValid) return;

        try
        {
            NewBid.Id = Guid.NewGuid();
            NewBid.Timestamp = DateTime.UtcNow;
            NewBid.ProductId = Product.Id;

            await BidService.CreateBidAsync(NewBid);

            IsPlacingBid = true;

            await BidFormRef.ResetAsync();

            // Increment local bidder number only
            BidderNumber++;
            NewBid = new BidViewModel
            {
                BidderName = $"Bidder {BidderNumber}",
                Amount = null
            };
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Failed to place bid: {ex.Message}", Severity.Error);
        }
    }

    private string FormatAmount(decimal amount) =>
        amount % 1 == 0
            ? ((int)amount).ToString()
            : amount.ToString("F2");

    public void Dispose()
    {
        CountdownTimer?.Stop();
        CountdownTimer?.Dispose();

        HubClientService.BidUpdated -= OnBidUpdated;
    }
}
