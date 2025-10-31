using Microsoft.AspNetCore.Identity;

namespace AuctionPortal.Data.Models;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsApprovedByAdmin { get; set; } = false;
}
