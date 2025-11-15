using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.Data.Models
{
    [Table("Products")]
    public class ProductModel
    {
        [Key]
        public Guid Id { get; set; }

        public ProductModel()
        {
            if (CreatedAt == DateTime.MinValue)
                CreatedAt = DateTime.UtcNow;
        }

        // ---------------------------
        // RELATIONSHIPS
        // ---------------------------
        [Required]
        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public AuctionModel Auction { get; set; } = null!;

        // ---------------------------
        // PRODUCT DETAILS
        // ---------------------------
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ReservePrice { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SaleSequence { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTime? SaleEnd { get; set; }

        // ---------------------------
        // SALE OUTCOME
        // ---------------------------
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FinalBid { get; set; }

        [MaxLength(100)]
        public string? FinalBidderName { get; set; }

        [NotMapped]
        public bool Sold => FinalBid.HasValue;

        // ---------------------------
        // TIMESTAMPS
        // ---------------------------
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ---------------------------
        // NAVIGATION COLLECTIONS
        // ---------------------------
        public List<BidModel> Bids { get; set; } = new();

        public List<ProductImageModel> Images { get; set; } = new();
    }
}
