using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace DashboardService.Seed
{
    public class CardConfiguration
    {
        public static void Seed(AppDbContext context)
        {            
            if (context.Cards.FirstOrDefault(x => x.Name == "Product") == null)
                context.Cards.Add(new Card() { Name = "Product" });

            context.SaveChanges();
        }
    }
}
