using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace APIGateway.SignalRHubs
{
    /// <summary>
    /// SignalR configuration for notifications
    /// </summary>
    [Authorize]
    public class NotificationsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();

            await AddToGroup(userId);

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = GetUserId();

            await RemoveFromGroup(userId);

            await base.OnDisconnectedAsync(exception);
        }

        public string GetUserId()
        {
            return Context.UserIdentifier;
        }
        private async Task AddToGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        private async Task RemoveFromGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }
    }
}
