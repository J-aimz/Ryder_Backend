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
        public int NumberOfUnreadMessages { get; set; }
        public MessageThreadParticipant MessageThreadParticipants { get; set; }
        //public Guid LastMessageId { get; set; }
        //public Guid PinnedMessageId { get; set; }
    }
}
