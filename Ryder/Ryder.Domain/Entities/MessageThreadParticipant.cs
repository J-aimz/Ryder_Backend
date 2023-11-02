
using Ryder.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ryder.Domain.Entities
{
    public class MessageThreadParticipant : BaseEntity
    {
        public MessageThread MessageThread { get; set; }
        [ForeignKey("MessageThreads")]
        public Guid MessageThreadId { get; set; }
        public Guid AppUserId { get; set; }
        public Guid? RiderId { get; set; }
       
    }
}
