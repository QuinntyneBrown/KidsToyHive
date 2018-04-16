using Core.Entities;
using Infrastructure.Data;
using System.Linq;

namespace ProductService.Seed
{
    public class ProductImageConfiguration
    {
        public static void Seed(AppDbContext context)
        {


            context.SaveChanges();
        }
    }
}
