using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace DashboardService.Seed
{
    public class CardConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            var card = context.Cards.FirstOrDefault(x => x.Name == "");

            if (card == null)
                context.Cards.Add(new Card() { Name = "" });

            context.SaveChanges();
        }
    }
}
