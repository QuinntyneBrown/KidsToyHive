using KidsToyHive.Core.Common;
using KidsToyHive.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models
{
    public class User: AggregateRoot
    {
        public User(Guid userId, string username = null, byte[] salt= null, string password = null) 
            => Apply(new UserCreated()
            {
                UserId = userId,
                Username = username,
                Password = password,
                Salt = salt
            });

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case UserCreated data:
                    UserId = data.UserId;
                    Username = data.Username;
                    Salt = data.Salt;
                    Password = data.Password;
                    RoleIds = new HashSet<Guid>();
                    break;
            }            
        }

        protected override void EnsureValidState()
        {

        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; private set; }

        public ICollection<Guid> RoleIds { get; set; }
    }
}
