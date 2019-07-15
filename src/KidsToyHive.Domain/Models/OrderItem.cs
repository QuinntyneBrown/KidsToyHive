using System;

namespace KidsToyHive.Domain.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }

        public string Name { get; set; }
        public int Version { get; set; }
    }
}
