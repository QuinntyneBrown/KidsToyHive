using System;

namespace KidsToyHive.Domain.Models
{
    public class CustomerShipTo
    {
        public Guid CustomerShipToId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ShipTo { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
