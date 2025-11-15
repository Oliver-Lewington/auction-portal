namespace AuctionPortal.ViewModels
{
    public class BidViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? BidderName { get; set; } 
        public decimal? Amount { get; set; }
        public string DisplayAmount => Amount.HasValue && Amount % 1 == 0 ? ((int)Amount).ToString() : Amount.Value.ToString("F2");
        public DateTime Timestamp { get; set; }
        public bool IsWinningBid { get; set; }
    }
}
