using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Ryder.Application.Common.Hubs
{
    public class NotificationHub : Hub, INotificationHub
    {
        public async Task NotifyRidersOfIncomingRequest(List<string> riderId)
        {
            await Clients.Clients(riderId).SendAsync("IncomingRequest", "You have an incoming request.");
        }


        public async Task NotifyUserOfRequestAccepted(string userId)
        {
            await Clients.User(userId).SendAsync("RequestAccepted", "Your request has been accepted.");
        }


        public async Task NotifyUserAndRiderOfOrderCompleted(string userId, string riderId)
        {
            await Clients.Users(userId, riderId).SendAsync("OrderCompleted", "order has been completed.");
        }

        public async Task NotifyUserNoRiderWasFound(string userId)
        {
            await Clients.User(userId).SendAsync("noRiderFound", "Please Try again later no rider was found");
        }
    }
}
