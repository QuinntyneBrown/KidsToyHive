using System;

namespace KidsToyHive.Domain.Models
{
    public class ShipmentLocation
    {
        public Guid ShipmentId { get; set; }
        public Guid? LocationId { get; set; }
        public Shipment Shipment { get; set; }
        public Location Location { get; set; }
    }
}
