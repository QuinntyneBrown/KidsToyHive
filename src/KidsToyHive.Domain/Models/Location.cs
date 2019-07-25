using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Domain.Models
{
    public class Location: BaseModel
    {
        public Guid LocationId { get; set; }
        public Address Adddress { get; set; }
        public string Description { get; set; }
        public LocationType Type { get; set; } = LocationType.Delivery;
    }
}
