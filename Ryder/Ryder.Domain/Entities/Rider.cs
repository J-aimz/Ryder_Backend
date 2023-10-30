using Ryder.Domain.Common;
using Ryder.Domain.Enums;

namespace Ryder.Domain.Entities
{
    public class Rider : BaseEntity
    {
        public string ValidIdUrl { get; set; }
        public string PassportPhoto { get; set; }
        public string BikeDocument { get; set; }
        public RiderAvailabilityStatus AvailabilityStatus { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
