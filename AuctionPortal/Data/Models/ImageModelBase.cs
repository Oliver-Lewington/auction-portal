namespace AuctionPortal.Data.Models
{
    public abstract class ImageModelBase
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = null!;
        public string? Alt { get; set; }
    }
}
