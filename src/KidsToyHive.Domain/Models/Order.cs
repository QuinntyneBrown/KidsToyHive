using System;

namespace KidsToyHive.Domain.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
