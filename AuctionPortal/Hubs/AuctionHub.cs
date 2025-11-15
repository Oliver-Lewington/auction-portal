using Microsoft.AspNetCore.SignalR;

namespace AuctionPortal.Hubs
{
    public class AuctionHub : Hub
    {
        // Join a group for a specific product
        public async Task JoinProductGroup(string productId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, productId);
        }

        // Leave a group for a specific product
        public async Task LeaveProductGroup(string productId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, productId);
        }
    }
}
