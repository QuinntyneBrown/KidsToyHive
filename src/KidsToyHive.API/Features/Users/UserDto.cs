using KidsToyHive.Core.Models;
using System;

namespace KidsToyHive.API.Features.Users
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public static UserDto FromUser(User user)
            => new UserDto
            {
                UserId = user.UserId,
                Username = user.Username
            };
    }
}
