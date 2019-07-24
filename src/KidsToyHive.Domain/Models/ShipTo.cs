using System;

namespace KidsToyHive.Domain.Models
{
    public class ShipTo
    {
        public Guid ShipToId { get; set; }
        public Address Adddress { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
    }
}
