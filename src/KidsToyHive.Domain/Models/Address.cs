using System;

namespace KidsToyHive.Domain.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
