using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
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
       

        //internal class CardLayoutConfiguration
        //{
        //    public static void Seed(AppDbContext context)
        //    {
        //        if (context.CardLayouts.FirstOrDefault(x => x.Name == "Poster") == null)
        //            context.CardLayouts.Add(new CardLayout() { Name = "Poster" });

        //        context.SaveChanges();
        //    }
        //}

        internal class CardConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.Cards.FirstOrDefault(x => x.Name == "Products") == null)
                    context.Cards.Add(new Card() { Name = "Products" });

                context.SaveChanges();
            }
        }

        internal class DashboardConfiguration {

            public static void Seed(AppDbContext context)
            {
                var profileId = new Guid("");
                if (context.Dashboards.FirstOrDefault(x=> x.Name == "Default" && x.ProfileId == profileId) == null)
                {
                    context.Dashboards.Add(new Dashboard()
                    {
                        Name = "Default",
                        ProfileId = profileId
                    });
                }

                context.SaveChanges();
            }
        }

        internal class DashboardCardConfiguration
        {

            public static void Seed(AppDbContext context)
            {
                var dashboard = context.Dashboards.Include(x => x.DashboardCards).First(x => x.ProfileId == 1);

                if (dashboard.DashboardCards.SingleOrDefault(x => x.CardId == 1) == null)
                {
                    dashboard.DashboardCards.Add(new DashboardCard()
                    {
                        CardId = 1,
                        Options = JsonConvert.SerializeObject(new DashboardCardApiModel.OptionsApiModel()
                        {
                            Top = 1,
                            Left = 1,
                            Width = 1,
                            Height = 1

                        })
                    });
                }
                
                context.SaveChanges();
            }
        }
        internal class UserConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                User user = default;

                if (context.Users.IgnoreQueryFilters().FirstOrDefault(x => x.Username == "quinntynebrown@gmail.com") == null)
                {
                    user = new User()
                    {
                        Username = "quinntynebrown@gmail.com"
                    };

                    user.Password = new PasswordHasher().HashPassword(user.Salt, "P@ssw0rd");

                    context.Users.Add(user);

                }
                
                context.Profiles.Add(new Profile()
                {
                    Name = "Quinntyne",
                    User = user
                });
                
                context.SaveChanges();
            }
        }

    }
}
