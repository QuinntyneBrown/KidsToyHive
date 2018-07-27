using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class Customer: AggregateRoot
    {
        public Customer(string name)
            => Apply(new CustomerCreated(name,CustomerId));

        public Guid CustomerId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case CustomerCreated customerCreated:
                    Name = customerCreated.Name;
					CustomerId = customerCreated.CustomerId;
                    break;

                case CustomerNameChanged customerNameChanged:
                    Name = customerNameChanged.Name;
                    break;

                case CustomerRemoved customerRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new CustomerNameChanged(name));

        public void Remove()
            => Apply(new CustomerRemoved());
    }
}
