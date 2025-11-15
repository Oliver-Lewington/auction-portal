using AuctionPortal.Services;

public static class DateTimeExtensions
{
    public static string GetTimeAgo(this DateTime timestamp)
    {
        var span = DateTime.UtcNow - timestamp.ToUniversalTime();

        if (span.TotalSeconds < 60)
            return $"{(int)span.TotalSeconds} seconds ago";
        if (span.TotalMinutes < 60)
            return $"{(int)span.TotalMinutes} minutes ago";
        if (span.TotalHours < 24)
            return $"{(int)span.TotalHours} hours ago";
        if (span.TotalDays < 7)
            return $"{(int)span.TotalDays} days ago";

        return timestamp.ToLocalTime().ToString("dd MMM yyyy");
    }
    /// <summary>
    /// Returns a friendly label for a target DateTime (e.g. "Today 19:02", "Tomorrow 19:02", "Monday 19:02", etc.)
    /// Modes:
    ///  - "auto" => Today/Tomorrow/Weekday (with time)
    ///  - "timeOnly" => HH:mm
    ///  - "weekdayOnly" => Monday (no time)
    ///  - "weekdayWithTime" => Monday 19:02
    /// </summary>
    public static string GetFriendlyDateLabel(this DateTime targetUtc, string? mode = null)
    {
        mode ??= "auto"; // options: "auto", "timeOnly", "weekdayOnly", "weekdayWithTime"
        var tz = DateTimeServices.GetUserTimeZone();

        // Convert the UTC (or unspecified) time to user's timezone
        // If targetUtc.Kind is Local, don't convert from Utc; we normalize by using ToUniversalTime then convert
        var targetUtcNormalized = (targetUtc.Kind == DateTimeKind.Utc) ? targetUtc : targetUtc.ToUniversalTime();
        var localTarget = TimeZoneInfo.ConvertTimeFromUtc(targetUtcNormalized, tz);

        var nowLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);

        var timePart = localTarget.ToString("HH:mm");
        var weekday = localTarget.ToString("dddd"); // e.g. Monday

        switch (mode)
        {
            case "timeOnly":
                return timePart;
            case "weekdayOnly":
                return weekday;
            case "weekdayWithTime":
                return $"{weekday} {timePart}";
            case "auto":
            default:
                if (localTarget.Date == nowLocal.Date)
                    return $"Today {timePart}";
                if (localTarget.Date == nowLocal.Date.AddDays(1))
                    return $"Tomorrow {timePart}";
                // within next 6 days show weekday, otherwise show full date
                if (localTarget.Date <= nowLocal.Date.AddDays(6))
                    return $"{weekday} {timePart}";
                // fallback: full date & time
                return localTarget.ToString("dd MMM yyyy HH:mm");
        }
    }
}
