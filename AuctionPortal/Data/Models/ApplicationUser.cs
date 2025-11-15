using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuctionPortal.Data.Models
{
    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsApprovedByAdmin { get; set; } = false;
    }
}
