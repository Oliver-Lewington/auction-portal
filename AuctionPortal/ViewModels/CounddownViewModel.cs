namespace AuctionPortal.ViewModels;

public class CountdownViewModel
{
    public CountdownViewModel()
    {
        
    }
    public CountdownViewModel(DateTime start, DateTime end)
    {
        var now = DateTime.UtcNow;

        if (now < start)
        {
            Progress = 0;
            IsExpired = false;

            return;
        }

        var remaining = end - now;
        if (remaining.TotalSeconds <= 0)
        {
            Progress = 100;
            IsExpired = true;

            return;
        }

        Days = remaining.Days;
        Hours = remaining.Hours;
        Minutes = remaining.Minutes;
        Seconds = remaining.Seconds;

        var totalDuration = end - start;
        var elapsed = now - start;

        Progress = Math.Clamp((elapsed.TotalSeconds / totalDuration.TotalSeconds) * 100, 0, 100);
    }

    public bool IsExpired { get; set; }
    public int Days { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public double Progress { get; set; }
}
