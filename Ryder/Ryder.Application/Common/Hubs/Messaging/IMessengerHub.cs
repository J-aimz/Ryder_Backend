using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Common.Hubs.Messaging
{
    public interface IMessengerHub
    {
        Task SendMessage(string receiverId, string messageBody);
        Task UpdateMessage(string receiversID, string message);
    }
}
