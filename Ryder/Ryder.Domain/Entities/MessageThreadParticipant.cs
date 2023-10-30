
using Ryder.Domain.Common;

namespace Ryder.Domain.Entities
{
    public class MessageThreadParticipant : BaseEntity
    {
        //public DateTime? LastReadTime { get; set; }
        public MessageThread MessageThread { get; set; }
        public Guid AppUserId { get; set; }
        public Guid RiderId { get; set; }
        //public bool IsPinned { get; set; }
        //public bool IsArchived { get; set; } = false;
        //public DateTime PinnedDate { get; set; }
    }
}
