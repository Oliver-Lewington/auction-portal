using System.ComponentModel.DataAnnotations;

namespace AuctionPortal.Data.Models
{
    /// <summary>
    /// Base class for all image entities.
    /// Contains shared properties for URLs, alternative text, and identifiers.
    /// </summary>
    public abstract class ImageModelBase
    {
        [Key]
        public Guid Id { get; set; }

        // Fully qualified URL or relative path to the stored image
        [Required]
        [MaxLength(2048)]
        public string Url { get; set; } = null!;

        // Optional alternative text for accessibility or SEO
        [MaxLength(255)]
        public string? Alt { get; set; }
    }
}
