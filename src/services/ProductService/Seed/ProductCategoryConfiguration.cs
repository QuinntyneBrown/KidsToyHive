using Core.Entities;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Seed
{
    public class ProductCategoryConfiguration
    {
        public static void Seed(AppDbContext context, Tenant tenant)
        {
            if (null == context.ProductCategories.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "Infant - 1 Year"))
                context.ProductCategories.Add(new ProductCategory() { Name = "Infant - 1 Year", Tenant = tenant });

            if (null == context.ProductCategories.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "1 Year - 2 Years"))
                context.ProductCategories.Add(new ProductCategory() { Name = "1 Year - 2 Years", Tenant = tenant });

            if (null == context.ProductCategories.IgnoreQueryFilters().FirstOrDefault(x => x.Name == "2 Years Plus"))
                context.ProductCategories.Add(new ProductCategory() { Name = "2 Years Plus", Tenant = tenant });

            context.SaveChanges();
        }
    }
}
