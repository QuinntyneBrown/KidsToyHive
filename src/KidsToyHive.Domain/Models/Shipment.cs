using System;

namespace KidsToyHive.Domain.Models
{
    public class Shipment: BaseModel
    {
        public Guid ShipmentId { get; set; }
        public string Name { get; set; }
        public Guid SignatureId { get; set; }
        public Signature Signature { get; set; }
    }
}
