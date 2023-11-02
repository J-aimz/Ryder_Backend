using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Common.Hubs.Messaging
{
    public class MessengerHub : Hub, IMessengerHub
    {

        public async Task SendMessage(string receverId, string messageBody)
        {
            await Clients.User(receverId).SendAsync("sendMessage", messageBody);
        }

        public async Task UpdateMessage(string receiversID, string message)
        {
            await Clients.User(receiversID).SendAsync("updateMessage", message);

        }


        public async Task SendPaymentToCustomer(string receiversID, string message)
        {
            await Clients.User(receiversID).SendAsync("sendUserPayementLink", message);
        }

    }
}
