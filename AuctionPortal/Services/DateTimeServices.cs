namespace AuctionPortal.Services
{
    public static class DateTimeServices
    {

        public static TimeZoneInfo GetUserTimeZone()
        {
            // Prefer IANA name; fallback to common Windows name; finally local
            try { return TimeZoneInfo.FindSystemTimeZoneById("Europe/London"); } catch { }
            try { return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"); } catch { }
            return TimeZoneInfo.Local;
        }
    }
}
