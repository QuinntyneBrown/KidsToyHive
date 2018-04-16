using Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ProductService.Seed;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args) 
            => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();        

        public static void SeedContext(AppDbContext context)
        {
            BrandConfiguration.Seed(context);
            ProductCategoryConfiguration.Seed(context);
            ProductConfiguration.Seed(context);            
        }
    }
}
