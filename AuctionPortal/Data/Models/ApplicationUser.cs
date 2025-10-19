using Microsoft.AspNetCore.Identity;

namespace AuctionPortal.Data.Models;
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
