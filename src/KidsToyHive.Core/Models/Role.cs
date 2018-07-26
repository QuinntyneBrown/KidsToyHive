using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;

namespace KidsToyHive.Core.Models
{
    public class Role: AggregateRoot
    {
        public Role(string name)
            => Apply(new RoleCreated(name,RoleId));

        public Guid RoleId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }        
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case RoleCreated roleCreated:
                    Name = roleCreated.Name;
                	RoleId = roleCreated.RoleId;
                    break;

                case RoleNameChanged roleNameChanged:
                    Name = roleNameChanged.Name;
                    break;

                case RoleRemoved roleRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new RoleNameChanged(name));

        public void Remove()
            => Apply(new RoleRemoved());
    }
}
