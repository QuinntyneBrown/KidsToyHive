using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace AccountService.Seed
{
    public class AccountConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            var account = context.Accounts.FirstOrDefault(x => x.Name == "");

            if (account == null)
                context.Accounts.Add(new Account() { Name = "" });

            context.SaveChanges();
        }
    }
}
