using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.Data.Models
{
    [Index(nameof(Name), IsUnique = false)]
    public class AuctionModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        // Navigation for the main auction image
        [Required]
        public AuctionImageModel Image { get; set; } = null!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        public ApplicationUser Creator { get; set; } = null!;

        [Required]
        public DateTime BeginsAt { get; set; }    // Stored as timestamptz

        [Required]
        public DateTime EndsAt { get; set; }      // Stored as timestamptz

        public bool LiveFlag { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; }   // DEFAULT now()

        [Required]
        public DateTime UpdatedAt { get; set; }   // updated via trigger or computed

        /// <summary>
        /// Time (in minutes) that each product in the auction remains active.
        /// </summary>
        [Range(1, 120)]
        public int ProductSaleTimeInterval { get; set; } = 5;

        // Relationships
        [InverseProperty(nameof(ProductModel.Auction))]
        public List<ProductModel> Products { get; set; } = new();
    }
}
