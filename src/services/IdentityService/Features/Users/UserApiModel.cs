using Core.Entities;
using System;

namespace IdentityService.Features.Users
{
    public class UserApiModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public static UserApiModel FromUser(User user)
        {
            var model = new UserApiModel();
            model.UserId = user.UserId;
            model.Username = user.Username;

            return model;
        }
    }
}
