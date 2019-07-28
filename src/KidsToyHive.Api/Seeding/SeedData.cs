using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            ProductConfiguration.Seed(context);
            WarehouseConfiguration.Seed(context);
            InventoryItemConfiguration.Seed(context);
            HtmlContentConfiguration.Seed(context);
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

        internal class HtmlContentConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.HtmlContents.FirstOrDefault(x => x.Name == "TermsAndConditions.html") == null)
                    context.HtmlContents.Add(new HtmlContent
                    {
                        Name = "TermsAndConditions.html",
                        Value = DigitalAssetLocator.GetString("TermsAndConditions.html")
                    });

                if (context.HtmlContents.FirstOrDefault(x => x.Name == "About.html") == null)
                    context.HtmlContents.Add(new HtmlContent
                    {
                        Name = "About.html",
                        Value = DigitalAssetLocator.GetString("About.html")
                    });

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
                    context.Products.Add(new Product
                    {
                        Name = "Jungle Jumparoo",
                        ProductImages = Get(new string [] {
                            "JungleJumparoo1.jpg",
                            "JungleJumparoo2.png",
                            "JungleJumparoo3.jpg",
                            "JungleJumparoo4.jpeg"
                        })
                    });

                context.SaveChanges();
            }

            private static ICollection<ProductImage> Get(string[] fileNames)
            {
                var productImages = new List<ProductImage>();

                foreach(var fileName in fileNames)
                {
                    var provider = new FileExtensionContentTypeProvider();
                    
                    provider.TryGetContentType(fileName, out string contentType);

                    productImages.Add(new ProductImage
                    {
                        DigitalAsset = new DigitalAsset()
                        {
                            Name = fileName,
                            ContentType = contentType,
                            Bytes = DigitalAssetLocator.Get(fileName)
                        }
                    });
                }

                return productImages;
            }

        }

        internal class InventoryItemConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                var product = context.Products.Single(x => x.Name == "Jungle Jumparoo");

                if (context.InventoryItems.Include(x => x.Product).FirstOrDefault(x => x.Product.Name == "Jungle Jumparoo") == null)
                    context.InventoryItems.Add(new InventoryItem
                    {
                        Quantity = 1,
                        ProductId = product.ProductId
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

                if (context.Cards.FirstOrDefault(x => x.Name == "Sales Orders") == null)
                    context.Cards.Add(new Card() { Name = "Sales Orders" });

                if (context.Cards.FirstOrDefault(x => x.Name == "Bookings") == null)
                    context.Cards.Add(new Card() { Name = "Bookings" });

                if (context.Cards.FirstOrDefault(x => x.Name == "Shipments") == null)
                    context.Cards.Add(new Card() { Name = "Shipments" });

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

        internal class WarehouseConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.Warehouses.FirstOrDefault(x => x.Name == "DefaultWarehouse") == null)
                    context.Warehouses.Add(new Warehouse() { Name = "DefaultWarehouse" });

                context.SaveChanges();
            }
        }

        internal class DigitalAssetLocator
        {
            public static string GetString(string name)
            {
                var lines = new List<string>();
                var fullName = default(string);
                var assembly = default(Assembly);
                var embededResourceNames = new List<string>();

                foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                {
                    foreach (Assembly _assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        try
                        {
                            foreach (var item in _assembly.GetManifestResourceNames()) embededResourceNames.Add(item);

                            if (!string.IsNullOrEmpty(_assembly.GetManifestResourceNames().SingleOrDefaultResourceName(name)))
                            {
                                fullName = _assembly.GetManifestResourceNames().SingleOrDefaultResourceName(name);
                                assembly = _assembly;
                            }
                        }
                        catch (System.NotSupportedException notSupportedException)
                        {
                            //swallow
                        }
                    }
                }

                if (fullName == default(string) && assembly == default(Assembly))
                    return null;

                try
                {
                    using (var stream = assembly.GetManifestResourceStream(fullName))
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            public static byte[] Get(string name)
            {
                var lines = new List<string>();
                var fullName = default(string);
                var assembly = default(Assembly);
                var embededResourceNames = new List<string>();

                foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                {
                    foreach (Assembly _assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        try
                        {
                            foreach (var item in _assembly.GetManifestResourceNames()) embededResourceNames.Add(item);

                            if (!string.IsNullOrEmpty(_assembly.GetManifestResourceNames().SingleOrDefaultResourceName(name)))
                            {
                                fullName = _assembly.GetManifestResourceNames().SingleOrDefaultResourceName(name);
                                assembly = _assembly;
                            }
                        }
                        catch (System.NotSupportedException notSupportedException)
                        {
                            //swallow
                        }
                    }
                }

                if (fullName == default(string) && assembly == default(Assembly))
                    return null;

                try
                {
                    using (var stream = assembly.GetManifestResourceStream(fullName))
                    {
                        MemoryStream ms = new MemoryStream();
                        stream.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }


    }

    public static class StringListExtensions
    {

        public static string SingleOrDefaultResourceName(this string[] collection, string name)
        {
            try
            {
                string result = null;

                if (collection.Length == 0) return null;

                result = collection.SingleOrDefault(x => x.EndsWith(name));

                if (result != null)
                    return result;

                return collection.SingleOrDefault(x => x.EndsWith($".{name}.txt"));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
