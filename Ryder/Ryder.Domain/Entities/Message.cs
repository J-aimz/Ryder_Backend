using Ryder.Domain.Common;

namespace Ryder.Domain.Entities
{
    public class Message : BaseEntity
    {
        public Guid MessageThreadId { get; set; }
        public string SenderId { get; set; } //This will be an app user Id
        public string ReceiverId { get; set; }
        public string Body { get; set; }
        public string? Emojie { get; set; }
    }
}
