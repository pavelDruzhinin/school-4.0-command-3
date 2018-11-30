using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Auctionator.Hubs
{
    public class AuctionHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} присоединился к аукциону");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} покинул аукцион");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
