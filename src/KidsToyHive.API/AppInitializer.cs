using KidsToyHive.Core.Identity;
using KidsToyHive.Core.Models;
using KidsToyHive.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Security.Cryptography;

namespace KidsToyHive.API
{
    public class AppInitializer: IDesignTimeDbContextFactory<AppDbContext>
    {
        public static void Seed(AppDbContext context)
        {
            var eventStore = new EventStore(context);

            if (eventStore.Query<User>("Username", "quinntynebrown@gmail.com") == null)
            {
                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var user = new User(new Guid("9f28229c-b39c-427e-8305-c1e07494d5d3"),
                    "quinntynebrown@gmail.com",
                    salt,
                    new PasswordHasher().HashPassword(salt, "P@ssw0rd")
                    );

                eventStore.Save(user);
            }

            if (eventStore.Query<Dashboard>("Name", "Default") == null)
            {
                var user = eventStore.Query<User>("Username", "quinntynebrown@gmail.com");

                var dashboard = new Dashboard("Default", user.UserId);

                eventStore.Save(dashboard);
            }

            if (eventStore.Query<Card>("Name", "Brands") == null)
                eventStore.Save(new Card("Brands"));

            if (eventStore.Query<Card>("Name", "Products") == null)
                eventStore.Save(new Card("Products"));

            if (eventStore.Query<ProductCategory>("Name", "Babies") == null)
                eventStore.Save(new ProductCategory("Babies"));

            if (eventStore.Query<ProductCategory>("Name", "Toddlers") == null)
                eventStore.Save(new ProductCategory("Toddlers"));

            if (eventStore.Query<ProductCategory>("Name", "Kids") == null)
                eventStore.Save(new ProductCategory("Kids"));

            context.SaveChanges();
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly)
                .Build();

            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"])
                .Options);
        }
    }
    
}
