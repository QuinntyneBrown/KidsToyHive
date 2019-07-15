using System;

namespace KidsToyHive.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
