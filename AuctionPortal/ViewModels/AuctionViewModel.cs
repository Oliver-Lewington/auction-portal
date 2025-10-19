namespace AuctionPortal.ViewModels;

public class AuctionViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool LiveFlag { get; set; }

    private DateTime _startDate = DateTime.Today.AddDays(1);
    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value.Date;
            UpdateBeginsAt();
        }
    }

    private TimeSpan _startTime = DateTime.UtcNow.TimeOfDay;
    public TimeSpan StartTime
    {
        get => _startTime;
        set
        {
            _startTime = value;
            UpdateBeginsAt();
        }
    }

    private DateTime _endDate = DateTime.Today.AddDays(2);
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value.Date;
            UpdateEndsAt();
        }
    }

    private TimeSpan _endTime = DateTime.UtcNow.AddHours(1).TimeOfDay;
    public TimeSpan EndTime
    {
        get => _endTime;
        set
        {
            _endTime = value;
            UpdateEndsAt();
        }
    }

    public DateTime BeginsAt { get; set; }
    public DateTime EndsAt { get; set; }

    public List<ProductViewModel> Products { get; set; } = new();

    public AuctionViewModel()
    {
        UpdateBeginsAt();
        UpdateEndsAt();
    }

    private void UpdateBeginsAt() => BeginsAt = StartDate.Date.Add(StartTime);
    private void UpdateEndsAt() => EndsAt = EndDate.Date.Add(EndTime);
}
