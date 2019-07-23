using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Domain.Models
{
    public class Profile
    {
        public Guid ProfileId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string AvatarUrl { get; set; }
        public ProfileType Type { get; set; }
        public int Version { get; set; }
        public User User { get; set; }
    }
}
