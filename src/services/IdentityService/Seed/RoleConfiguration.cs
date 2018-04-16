using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace IdentityService.Seed
{
    public class RoleConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            var systemRole = context.Roles.FirstOrDefault(x => x.Name == "SYSTEM");

            if (systemRole == null)
                context.Roles.Add(new Role() { Name = "SYSTEM" });

            context.SaveChanges();
        }
    }
}
