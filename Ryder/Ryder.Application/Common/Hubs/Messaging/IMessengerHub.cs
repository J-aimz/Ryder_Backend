using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Common.Hubs.Messaging
{
    public interface IMessengerHub
    {
        Task SendMessage(string receverId, string messageBody);
    }
}
