using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionPortal.Data.Models
{
    public class ProductImageModel : ImageModelBase
    {
        [Key]
        public Guid Id { get; set; }

        // Optional short description or caption for the image
        [MaxLength(255)]
        public string? Caption { get; set; }

        // Foreign key linking this image to its product
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductModel Product { get; set; } = null!;
    }
}
