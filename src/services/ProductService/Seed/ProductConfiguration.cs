using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace ProductService.Seed
{
    public class ProductConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            var threeStageActivityCenter = context.Products.FirstOrDefault(x => x.Name == "Three-Stage Activity Center");

            if (threeStageActivityCenter == null)
            {
                threeStageActivityCenter = new Product
                {
                    Brand = context.Brands.First(x => x.Name == "Skip Hop"),
                    Color = "Multi",
                    ProductCategory = context.ProductCategories.First( x=> x.Name == "Infant - 1 Year")
                };

                context.Products.Add(threeStageActivityCenter);
            }

            var playCube = context.Products.FirstOrDefault(x => x.Name == "Country Critters Play Cube");

            if(playCube == null)
            {
                playCube = new Product()
                {
                    Name = "Country Critters Play Cube",
                    Brand = context.Brands.First(x => x.Name == "Hape Toys"),
                    ProductCategory = context.ProductCategories.First(x => x.Name == "1 Year - 2 Years")
                };

                context.Products.Add(playCube);
            }

            var workbench = context.Products.FirstOrDefault(x => x.Name == "Master Workbench");

            if (workbench == null)
            {
                workbench = new Product()
                {
                    Name = "Master Workbench",
                    Color = "Multi",
                    Brand = context.Brands.First(x => x.Name == "Hape Toys"),
                    ProductCategory = context.ProductCategories.First(x => x.Name == "2 Years Plus")
                };

                context.Products.Add(workbench);
            }

            context.SaveChanges();
        }
    }
}
