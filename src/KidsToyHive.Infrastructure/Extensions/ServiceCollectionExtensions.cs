using KidsToyHive.Core.Interfaces;
using KidsToyHive.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KidsToyHive.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {                
        public static IServiceCollection AddDataStore(this IServiceCollection services,
                                               string connectionString, bool useInMemoryDatabase = false)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();

            return services.AddDbContext<AppDbContext>(options =>
            {
                _ = useInMemoryDatabase 
                ? options.UseInMemoryDatabase(databaseName: "KidsToyHive")
                : options.UseSqlServer(connectionString, b => b.MigrationsAssembly("KidsToyHive.Infrastructure"));
            });          
        }
    }
}
