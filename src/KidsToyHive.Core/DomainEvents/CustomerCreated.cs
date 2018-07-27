using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class CustomerCreated: DomainEvent
    {
        public CustomerCreated(string name, Guid customerId)
        {
            Name = name;
            CustomerId = customerId;
        }
        public string Name { get; set; }
        public Guid CustomerId { get; set; }
    }
}
