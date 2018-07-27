using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class OrderItemCreated: DomainEvent
    {
        public OrderItemCreated(string name, Guid orderItemId)
        {
            Name = name;
            OrderItemId = orderItemId;
        }
        public string Name { get; set; }
        public Guid OrderItemId { get; set; }
    }
}
