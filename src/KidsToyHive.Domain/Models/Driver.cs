using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Driver: BaseModel
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
        = new HashSet<Shipment>();
    }
}
