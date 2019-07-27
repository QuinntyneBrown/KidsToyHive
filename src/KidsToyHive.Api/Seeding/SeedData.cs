﻿using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace KidsToyHive.Api
{
    public class SeedData
    {
        public static void Seed(AppDbContext context, IConfiguration configuration)
        {
            CardConfiguration.Seed(context);
            CardLayoutConfiguration.Seed(context);
            UserConfiguration.Seed(context, configuration);
            TaxConfiguration.Seed(context);
            DriverConfiguration.Seed(context, configuration);
            //DashboardConfiguration.Seed(context);
        }


        internal class CardLayoutConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.CardLayouts.FirstOrDefault(x => x.Name == "Poster") == null)
                    context.CardLayouts.Add(new CardLayout() { Name = "Poster" });

                context.SaveChanges();
            }
        }

        internal class DriverConfiguration
        {
            public static void Seed(AppDbContext context, IConfiguration configuration)
            {
                foreach(var user in context.Users)
                {
                    if(configuration["Seed:DefaultUser:Username"].Contains(user.Username))
                    {
                        if (context.Drivers.Where(x => x.Email == user.Username).FirstOrDefault() == null)
                        {
                            context.Drivers.Add(new Driver
                            {
                                Email = user.Username
                            });
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        internal class TaxConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.Taxes.FirstOrDefault() == null)
                    context.Taxes.Add(new Tax() { Rate = 13 });

                context.SaveChanges();
            }
        }

        internal class ProductConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.Products.FirstOrDefault() == null)
                    context.Products.Add(new Product {

                    });

                context.SaveChanges();
            }
        }

        internal class CardConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.Cards.FirstOrDefault(x => x.Name == "Products") == null)
                    context.Cards.Add(new Card() { Name = "Products" });

                if (context.Cards.FirstOrDefault(x => x.Name == "Orders") == null)
                    context.Cards.Add(new Card() { Name = "Orders" });

                if (context.Cards.FirstOrDefault(x => x.Name == "Inventory") == null)
                    context.Cards.Add(new Card() { Name = "Inventory" });

                context.SaveChanges();
            }
        }

        internal class DashboardConfiguration {

            public static void Seed(AppDbContext context)
            {                
                foreach(var profileId in context.Profiles.Select(x => x.ProfileId))
                {
                    if (context.Dashboards.FirstOrDefault(x => x.Name == "Default" && x.ProfileId == profileId) == null)
                    {
                        context.Dashboards.Add(new Dashboard()
                        {
                            Name = "Default",
                            ProfileId = profileId
                        });
                    }
                }

                context.SaveChanges();
            }
        }

        internal class UserConfiguration
        {
            public static void Seed(AppDbContext context, IConfiguration configuration) {

                var index = 0;

                foreach (var username in configuration["Seed:DefaultUser:Username"].Split(','))
                {
                    User user = default;

                    if (context.Users.FirstOrDefault(x => x.Username == username) == null)
                    {
                        user = new User()
                        {
                            Username = username
                        };

                        user.Password = new PasswordHasher().HashPassword(user.Salt, configuration["Seed:DefaultUser:Password"]);

                        context.Users.Add(user);
                    }

                    context.Profiles.Add(new Profile()
                    {
                        Name = configuration["Seed:DefaultProfile:Name"].Split(',')[index],
                        User = user
                    });

                    context.SaveChanges();
                    index++;
                }

                index++;
            }
        }

    }
}