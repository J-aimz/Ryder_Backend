using Ryder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryder.Application.Order.Query.GetAll
{
    public class GetAllQueryResponse
    {
        public Guid OrderId { get; set; }
        public string PickUpLocationAddressDescription { get; set; }
        public string DropOffLocationAddressDescription { get; set; }
        public string PackageDescription { get; set; }
        public string Phone { get; set; }
        public string DropLongitude { get; set; }
        public string DropLatitude { get; set; }
        public string PickLatitude { get; set; }
        public string PickLongitude { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public OrderStatus Status { get; set; }
    }
}
