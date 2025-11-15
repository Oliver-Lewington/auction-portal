using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.Data.Models
{
    /// <summary>
    /// Represents a single bid placed on a product within an auction.
    /// </summary>
    public class BidModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? BidderName { get; set; }

        // The amount offered in the bid
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid amount must be greater than zero.")]
        public decimal Amount { get; set; }

        // The exact UTC timestamp when the bid was placed
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Indicates whether this bid is currently the winning bid
        [Required]
        public bool IsWinningBid { get; set; } = false;

        // Foreign key relationship to Product
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductModel Product { get; set; } = null!;
    }
}
