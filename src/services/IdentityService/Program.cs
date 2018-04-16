using IdentityService.Seed;
using Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace IdentityService
{
    public class Program
    {
        public static void Main(string[] args) 
            => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();        

        public static void SeedContext(AppDbContext context, IConfiguration configuration)
        {
            RoleConfiguration.Seed(context);
            UserConfiguration.Seed(context, configuration);
        }
    }
}
