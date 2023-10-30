using Ryder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Messages.Query.GetTheadMessages
{
    public class MessageThreadDto
    {
        public string MessageThread { get; set; }
        public string OrderId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
