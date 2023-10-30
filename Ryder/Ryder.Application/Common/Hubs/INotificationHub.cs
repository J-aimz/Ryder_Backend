using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Common.Hubs
{
    public interface INotificationHub
    {
        Task NotifyUserOfRequestAccepted(string userId);
        Task NotifyRidersOfIncomingRequest(List<string> riderId);
        Task NotifyUserAndRiderOfOrderCompleted(string userId, string riderId);
        Task NotifyUserNoRiderWasFound(string userId);
    }
}
