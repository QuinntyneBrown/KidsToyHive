using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace KidsToyHive.Admin.SPA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder().Build();

            ProcessDbCommands(args, host);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();

        public static void ProcessDbCommands(string[] args, IWebHost host) {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            
            using (var scope = services.CreateScope())
            {                
                if (args.Contains("seeddb"))
                {
                    var appDbContext = GetDbContext(scope);

                    var tenant = appDbContext.Tenants.SingleOrDefault(x => x.Name == "Default");

                    if (tenant == null) { appDbContext.Tenants.Add(tenant = new Tenant() { Name = "Default" }); }
                    
                    SeedContext(GetDbContext(scope),GetConfiguration(scope));
                }

                if (args.Contains("migratedb"))
                {
                    GetDbContext(scope).Database.Migrate();
                }

                if (args.Contains("dropdb"))
                {                    
                    GetDbContext(scope).Database.EnsureDeleted();
                }

                if (args.Contains("stop"))
                {
                    Environment.Exit(0);
                }
            }
        }

        public static AppDbContext GetDbContext(IServiceScope serviceScope)
            => serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        public static IHttpContextAccessor GetHttpContextAccessor(IServiceScope serviceScope)
            => serviceScope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

        public static IConfiguration GetConfiguration(IServiceScope serviceScope)
            => serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();

        public static void SeedContext(AppDbContext context, IConfiguration configuration)
        {
            IdentityService.Program.SeedContext(context, configuration);
            DashboardService.Program.SeedContext(context);
            ProductService.Program.SeedContext(context,null);
        }
    }
}
