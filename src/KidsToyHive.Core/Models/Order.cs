using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class Order: AggregateRoot
    {
        public Order(string name)
            => Apply(new OrderCreated(name,OrderId));

        public Guid OrderId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case OrderCreated orderCreated:
                    Name = orderCreated.Name;
					OrderId = orderCreated.OrderId;
                    break;

                case OrderNameChanged orderNameChanged:
                    Name = orderNameChanged.Name;
                    break;

                case OrderRemoved orderRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new OrderNameChanged(name));

        public void Remove()
            => Apply(new OrderRemoved());
    }
}
