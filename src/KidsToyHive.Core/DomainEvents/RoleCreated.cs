using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class RoleCreated: DomainEvent
    {
        public RoleCreated(string name, Guid roleId) {
            Name = name;
            RoleId = roleId;
        }
        public string Name { get; set; }
        public Guid RoleId { get; set; }
    }
}
