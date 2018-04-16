using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace ProductService.Seed
{
    public class BrandConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Brands.FirstOrDefault(x => x.Name == "Skip Hop") == null)
                context.Brands.Add(new Brand() { Name = "Skip Hop" });

            if (context.Brands.FirstOrDefault(x => x.Name == "Hape Toys") == null)
                context.Brands.Add(new Brand() { Name = "Hape Toys" });
            
            context.SaveChanges();
        }
    }
}
