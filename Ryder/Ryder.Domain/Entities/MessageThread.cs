using Ryder.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ryder.Domain.Entities
{
    public class MessageThread : BaseEntity
    {
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Orders { get; set; }
        public bool MessageIsRead { get; set; } = false;
        public int NumberOfUnreadMessages { get; set; } = 0;
        public MessageThreadParticipant MessageThreadParticipants { get; set; }
        [ForeignKey("MessageThreadParticipants")]
        public Guid? MessageThreadParticipantsId { get; set; } 
        public Message Messages { get; set; }
    }
}
