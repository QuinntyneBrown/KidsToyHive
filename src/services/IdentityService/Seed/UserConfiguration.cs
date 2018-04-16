using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace IdentityService.Seed
{
    public class UserConfiguration
    {
        public static void Seed(AppDbContext context, IConfiguration configuration)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == "quinntynebrown@gmail.com");

            if (user == null)
            {
                var encryptionService = new EncryptionService(configuration);
                context.Users.Add(new User()
                {
                    Username = "quinntynebrown@gmail.com",
                    Password = encryptionService.TransformPassword("P@ssw0rd"),
                    Firstname = "Quinntyne",
                    Lastname = "Brown"
                });
            }
            
            context.SaveChanges();
        }
    }
}
