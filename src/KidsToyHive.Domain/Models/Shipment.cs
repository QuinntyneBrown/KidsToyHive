using System;

namespace KidsToyHive.Domain.Models
{
    public class Shipment
    {
        public Guid ShipmentId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
