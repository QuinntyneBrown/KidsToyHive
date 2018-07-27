using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class OrderItem: AggregateRoot
    {
        public OrderItem(string name)
            => Apply(new OrderItemCreated(name,OrderItemId));

        public Guid OrderItemId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case OrderItemCreated orderItemCreated:
                    Name = orderItemCreated.Name;
					OrderItemId = orderItemCreated.OrderItemId;
                    break;

                case OrderItemNameChanged orderItemNameChanged:
                    Name = orderItemNameChanged.Name;
                    break;

                case OrderItemRemoved orderItemRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new OrderItemNameChanged(name));

        public void Remove()
            => Apply(new OrderItemRemoved());
    }
}
