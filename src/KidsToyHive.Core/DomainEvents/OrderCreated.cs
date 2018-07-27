using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class OrderCreated: DomainEvent
    {
        public OrderCreated(string name, Guid orderId) {
            Name = name;
            OrderId = orderId;
        }
        public string Name { get; set; }
        public Guid OrderId { get; set; }
    }
}
