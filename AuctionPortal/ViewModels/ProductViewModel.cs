using System;
using System.Collections.Generic;

namespace AuctionPortal.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public Guid AuctionId { get; set; }

    public ProductViewModel(Guid auctionId)
    {
        AuctionId = auctionId;
    }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal? ReservePrice { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public decimal? FinalBid { get; set; }
    public string? FinalBidderName { get; set; }
    public bool Sold => FinalBid.HasValue;

    public List<ImageViewModel> Images { get; set; } = new();
}
