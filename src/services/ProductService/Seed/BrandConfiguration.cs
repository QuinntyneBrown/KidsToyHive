using Core.Entities;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Seed
{
    public class BrandConfiguration
    {
        public static void Seed(AppDbContext context, Tenant tenant)
        {
            if (context.Brands.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "Skip Hop") == null)
                context.Brands.Add(new Brand() { Name = "Skip Hop", Tenant = tenant });

            if (context.Brands.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "Hape Toys") == null)
                context.Brands.Add(new Brand() { Name = "Hape Toys", Tenant = tenant });
            
            context.SaveChanges();
        }
    }
}
