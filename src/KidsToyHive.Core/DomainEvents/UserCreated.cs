using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class UserCreated: DomainEvent
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public string Password { get; set; }
    }
}
