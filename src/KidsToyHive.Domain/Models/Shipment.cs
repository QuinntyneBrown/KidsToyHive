using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Domain.Models
{
    public class Shipment: BaseModel
    {
        public Guid ShipmentId { get; set; }
        public Guid LocationId { get; set; }             
        public Guid SignatureId { get; set; }
        public Location Location { get; set; }
        public Signature Signature { get; set; }
        public ShipmentType Type { get; set; } = ShipmentType.Delivery;
    }
}
