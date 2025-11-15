using Microsoft.AspNetCore.SignalR.Client;
using AuctionPortal.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionPortal.Services;

public class AuctionHubClientService
{
    private readonly NavigationManager _navigation;
    private readonly HubConnection hubConnection;

    public event Action<BidViewModel>? BidUpdated;

    public AuctionHubClientService(NavigationManager navigation)
    {
        _navigation = navigation;

        hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigation.ToAbsoluteUri("/auctionHub"))
            .WithAutomaticReconnect()
            .Build();

        // Register only once in constructor
        hubConnection.On<BidViewModel>("BidUpdated", (bid) =>
        {
            BidUpdated?.Invoke(bid);
        });
    }

    public async Task StartAsync()
    {
        if (hubConnection.State == HubConnectionState.Disconnected)
        {
            await hubConnection.StartAsync();
        }
    }

    public async Task JoinProductGroup(Guid productId)
    {
        if (hubConnection.State == HubConnectionState.Connected)
        {
            await hubConnection.SendAsync("JoinProductGroup", productId);
        }
    }

    public async Task LeaveProductGroup(Guid productId)
    {
        if (hubConnection.State == HubConnectionState.Connected)
        {
            await hubConnection.SendAsync("LeaveProductGroup", productId);
        }
    }

    public bool IsConnected =>
        hubConnection.State == HubConnectionState.Connected;

    public HubConnection Connection => hubConnection;
}
