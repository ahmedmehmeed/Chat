using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChattingApp.SignalR
{
   
    public class PresenceHub:Hub
    {
        private readonly PresenceTracker presenceTracker;
        public PresenceHub(PresenceTracker presenceTracker)
        {
            this.presenceTracker = presenceTracker;
        }
        public override async Task OnConnectedAsync()
        {
           
                var userName = Context.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                await presenceTracker.UserConnected(userName, Context.ConnectionId);
                await Clients.Others.SendAsync("UserIsOnline", userName);
                var currentOlineUsers = await presenceTracker.GetOnlineUsers();
                await Clients.All.SendAsync("OlineUsers", currentOlineUsers);
 
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userName = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await presenceTracker.UserDisconnected(userName, Context.ConnectionId);
            var currentOlineUsers = await presenceTracker.GetOnlineUsers();
            await Clients.All.SendAsync("OlineUsers", currentOlineUsers);

            await Clients.Others.SendAsync("UserIsOffline", userName);
            await base.OnDisconnectedAsync(exception);

        }
    }
}
