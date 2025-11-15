using AuctionPortal.Data.Models;
using System;
using System.Collections.Generic;

namespace AuctionPortal.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public Guid AuctionId { get; set; }
    public AuctionViewModel Auction { get; set; } = null!;

    public ProductViewModel(Guid auctionId)
    {
        AuctionId = auctionId;
    }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal ReservePrice { get; set; } = 0;
    public int SaleSequence { get; set; }

    public decimal? FinalBid => Bids.OrderBy(b => b.Timestamp).LastOrDefault()?.Amount;
    public string? FinalBidderName { get; set; }
    public bool Sold => FinalBid.HasValue;

    public DateTime SaleEnd { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<ImageViewModel> Images { get; set; } = new();
    public List<BidViewModel> Bids { get; set; } = new();
}
