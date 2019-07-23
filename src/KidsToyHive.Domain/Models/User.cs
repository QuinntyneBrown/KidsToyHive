using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace KidsToyHive.Domain.Models
{
    public class User
    {
        public User()
        {
            Salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Salt);
            }
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; private set; }
        public ICollection<Profile> Profiles { get; set; }
        = new HashSet<Profile>();
        public int Version { get; set; }
    }
}
