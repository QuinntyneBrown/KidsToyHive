using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.DashboardCards;
using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KidsToyHive.Api
{
    public class SeedData
    {
        public static void Seed(AppDbContext context)
        {

            CardConfiguration.Seed(context);
            //CardLayoutConfiguration.Seed(context);

            //UserConfiguration.Seed(context);
            //TagConfiguration.Seed(context);

            //DashboardConfiguration.Seed(context);
            //DashboardCardConfiguration.Seed(context);
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
                var profileIds = new List<Guid>()
                {
                    new Guid("3ef4e425-f501-4034-a0fe-b87a770fda18"),
                    new Guid("bca1636a-1e32-4e64-b798-9dbb474284bf")
                };

                foreach(var profileId in profileIds)
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
            public static void Seed(AppDbContext context) {

                var passwords = new List<string>()
                {
                    "quinntynebrown@gmail.com",
                    "vanessamitchell88@gmail.com"
                };

                var emails = new List<string>()
                {
                    "quinntynebrown@gmail.com",
                    "vanessamitchell88@gmail.com"
                };

                var userIds = new List<Guid>()
                {
                    new Guid("3096b802-b9bf-4d7d-8c10-0611fd9e9ab6"),
                    new Guid("851b8982-ff49-4b64-a799-215957388900")
                };

                var profileNames = new List<string>()
                {
                    "Quinntyne",
                    "Vanessa"
                };

                var profileIds = new List<Guid>()
                {
                    new Guid("3ef4e425-f501-4034-a0fe-b87a770fda18"),
                    new Guid("bca1636a-1e32-4e64-b798-9dbb474284bf")
                };

                var index = 0;

                foreach(var email in emails)
                {
                    
                    User user = default;

                    if (context.Users.IgnoreQueryFilters().FirstOrDefault(x => x.Username == email) == null)
                    {
                        user = new User()
                        {
                            Username = email
                        };

                        user.Password = new PasswordHasher().HashPassword(user.Salt, passwords[index]);

                        context.Users.Add(user);
                    }

                    context.Profiles.Add(new Profile()
                    {
                        Name = profileNames[index],
                        ProfileId = profileIds[index],
                        User = user
                    });

                    context.SaveChanges();
                }

                index++;
            }
        }

    }
}
