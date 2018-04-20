using Core.Entities;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Seed
{
    public class ProductConfiguration
    {
        public static void Seed(AppDbContext context, Tenant tenant)
        {
            var threeStageActivityCenter = context.Products.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "Three-Stage Activity Center");

            if (threeStageActivityCenter == null)
            {
                threeStageActivityCenter = new Product
                {
                    Name = "Three-Stage Activity Center",
                    Brand = context.Brands.IgnoreQueryFilters().First(x => x.Name == "Skip Hop"),
                    Color = "Multi",
                    ProductCategory = context.ProductCategories.IgnoreQueryFilters().First( x=> x.Name == "Infant - 1 Year"),
                    Tenant = tenant
                };

                context.Products.Add(threeStageActivityCenter);
            }

            var playCube = context.Products.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "Country Critters Play Cube");

            if (playCube == null)
            {
                playCube = new Product()
                {
                    Name = "Country Critters Play Cube",
                    Brand = context.Brands.IgnoreQueryFilters().First(x => x.Name == "Hape Toys"),
                    ProductCategory = context.ProductCategories.IgnoreQueryFilters().First(x => x.Name == "1 Year - 2 Years"),
                    Tenant = tenant
                };

                context.Products.Add(playCube);
            }

            var workbench = context.Products.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "Master Workbench");

            if (workbench == null)
            {
                workbench = new Product()
                {
                    Name = "Master Workbench",
                    Color = "Multi",
                    Brand = context.Brands.IgnoreQueryFilters().First(x => x.Name == "Hape Toys"),
                    ProductCategory = context.ProductCategories.IgnoreQueryFilters().First(x => x.Name == "2 Years Plus"),
                    Tenant = tenant
                };

                context.Products.Add(workbench);
            }

            context.SaveChanges();
        }
    }
}
