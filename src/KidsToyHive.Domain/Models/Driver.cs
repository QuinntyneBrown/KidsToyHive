using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Driver: BaseModel
    {
        public Guid DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
        = new HashSet<Shipment>();
    }
}
