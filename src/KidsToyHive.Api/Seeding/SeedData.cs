using KidsToyHive.Core.Enums;
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

namespace KidsToyHive.Api;

public class SeedData
{
    public static void Seed(AppDbContext context, IConfiguration configuration)
    {
        TenantConfiguration.Seed(context, configuration);
        CardConfiguration.Seed(context);
        CardLayoutConfiguration.Seed(context);
        UserConfiguration.Seed(context, configuration);
        TaxConfiguration.Seed(context);
        DriverConfiguration.Seed(context, configuration);
        ProductConfiguration.Seed(context);
        WarehouseConfiguration.Seed(context);
        InventoryItemConfiguration.Seed(context);
        HtmlContentConfiguration.Seed(context);
        DigitalAssetConfiguration.Seed(context);
        EmailTemplateConfiguration.Seed(context);
        VideoConfiguration.Seed(context);
        ProfessionalServiceProviderConfiguration.Seed(context);
        //DashboardConfiguration.Seed(context);
    }
    internal class TenantConfiguration
    {
        public static void Seed(AppDbContext context, IConfiguration configuration)
        {
            var tenantInfos = configuration["Seed:DefaultTenants"].Split(',');
            foreach (var tenantInfo in tenantInfos)
            {
                var tenantId = new Guid(tenantInfo.Split('|')[1]);
                var name = tenantInfo.Split('|')[0];
                if (context.Tenants.SingleOrDefault(x => x.Name == name) == null)
                    context.Tenants.Add(new Tenant()
                    {
                        Name = name,
                        TenantId = tenantId
                    });
            }
            context.SaveChanges();
        }
    }
    internal class ProfessionalServiceProviderConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            var tenant = context.Tenants.Single(x => x.Name == "Code With QB");
            if (context.ProfessionalServiceProviders.SingleOrDefault(x => x.FullName == "Quinntyne Brown") == null)
                context.ProfessionalServiceProviders.Add(new ProfessionalServiceProvider()
                {
                    FullName = "Quinntyne Brown",
                    Title = "Architect and Senior Software Engineer",
                    ImageUrl = "https://avatars0.githubusercontent.com/u/1749159?s=400&u=b36e138431ef4f0a383e51eef90248ad07066b28&v=4",
                    TenantId = tenant.TenantId
                });
            context.SaveChanges();
        }
    }
    internal class VideoConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Videos.SingleOrDefault(x => x.Title == "The Kids Toy Hive") == null)
                context.Videos.Add(new Video()
                {
                    Title = "The Kids Toy Hive"
                });
            context.SaveChanges();
        }
    }
    internal class CardLayoutConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.CardLayouts.SingleOrDefault(x => x.Name == "Poster") == null)
                context.CardLayouts.Add(new CardLayout() { Name = "Poster" });
            context.SaveChanges();
        }
    }
    internal class DriverConfiguration
    {
        public static void Seed(AppDbContext context, IConfiguration configuration)
        {
            foreach (var user in context.Users.Include(x => x.Profiles))
            {
                if (configuration["Seed:DefaultUser:Username"].Contains(user.Username))
                {
                    if (context.Drivers.Where(x => x.Email == user.Username).SingleOrDefault() == null)
                    {
                        context.Drivers.Add(new Driver
                        {
                            Email = user.Username
                        });
                    }
                    if (user.Profiles.Where(x => x.Type == ProfileType.Driver).SingleOrDefault() == null)
                    {
                        user.Profiles.Add(new Profile
                        {
                            Type = ProfileType.Driver
                        });
                    }
                }
            }
            context.SaveChanges();
        }
    }
    internal class EmailTemplateConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.EmailTemplates.SingleOrDefault(x => x.Name == nameof(EmailTemplateName.BookingConfirmation)) == null)
                context.EmailTemplates.Add(new EmailTemplate
                {
                    Name = nameof(EmailTemplateName.BookingConfirmation),
                    Value = StaticFileLocator.GetAsString("BookingConfirmationEmail.html")
                });
            if (context.EmailTemplates.SingleOrDefault(x => x.Name == nameof(EmailTemplateName.NewCustomer)) == null)
                context.EmailTemplates.Add(new EmailTemplate
                {
                    Name = nameof(EmailTemplateName.NewCustomer),
                    Value = StaticFileLocator.GetAsString("NewCustomerEmail.html")
                });
            context.SaveChanges();
        }
    }
    internal class HtmlContentConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.HtmlContents.SingleOrDefault(x => x.Name == "TermsAndConditions.html") == null)
                context.HtmlContents.Add(new HtmlContent
                {
                    Name = "TermsAndConditions.html",
                    Value = StaticFileLocator.GetAsString("TermsAndConditions.html")
                });
            if (context.HtmlContents.SingleOrDefault(x => x.Name == "About.html") == null)
                context.HtmlContents.Add(new HtmlContent
                {
                    Name = "About.html",
                    Value = StaticFileLocator.GetAsString("About.html")
                });
            context.SaveChanges();
        }
    }
    internal class TaxConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Taxes.SingleOrDefault() == null)
                context.Taxes.Add(new Tax() { Rate = .13 });
            context.SaveChanges();
        }
    }
    internal class DigitalAssetConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.DigitalAssets.SingleOrDefault(x => x.Name == "Logo.png") == null)
            {
                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType("KidsToyHiveLogo.png", out string contentType);
                context.DigitalAssets.Add(new DigitalAsset
                {
                    Name = "Logo.png",
                    Bytes = StaticFileLocator.Get("KidsToyHiveLogo.png"),
                    ContentType = contentType
                });
                context.SaveChanges();
            }
            if (context.DigitalAssets.SingleOrDefault(x => x.Name == "Hero1.jpg") == null)
            {
                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType("Hero1.jpg", out string contentType);
                context.DigitalAssets.Add(new DigitalAsset
                {
                    Name = "Hero1.jpg",
                    Bytes = StaticFileLocator.Get("Hero1.jpg"),
                    ContentType = contentType
                });
                context.SaveChanges();
            }
            if (context.DigitalAssets.SingleOrDefault(x => x.Name == "Hero2.jpg") == null)
            {
                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType("Hero2.jpg", out string contentType);
                context.DigitalAssets.Add(new DigitalAsset
                {
                    Name = "Hero2.jpg",
                    Bytes = StaticFileLocator.Get("Hero2.jpg"),
                    ContentType = contentType
                });
                context.SaveChanges();
            }
            if (context.DigitalAssets.SingleOrDefault(x => x.Name == "Hero3.jpg") == null)
            {
                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType("Hero3.jpg", out string contentType);
                context.DigitalAssets.Add(new DigitalAsset
                {
                    Name = "Hero3.jpg",
                    Bytes = StaticFileLocator.Get("Hero3.jpg"),
                    ContentType = contentType
                });
                context.SaveChanges();
            }
        }
    }
    internal class ProductConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Products.SingleOrDefault() == null)
                context.Products.Add(new Product
                {
                    Name = "Jungle Jumparoo",
                    ProductImages = Get(new string[] {
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
            foreach (var fileName in fileNames)
            {
                var provider = new FileExtensionContentTypeProvider();

                provider.TryGetContentType(fileName, out string contentType);
                productImages.Add(new ProductImage
                {
                    DigitalAsset = new DigitalAsset()
                    {
                        Name = fileName,
                        ContentType = contentType,
                        Bytes = StaticFileLocator.Get(fileName)
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
            if (context.InventoryItems.Include(x => x.Product).SingleOrDefault(x => x.Product.Name == "Jungle Jumparoo") == null)
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
            if (context.Cards.SingleOrDefault(x => x.Name == "Products") == null)
                context.Cards.Add(new Card() { Name = "Products" });
            if (context.Cards.SingleOrDefault(x => x.Name == "Sales Orders") == null)
                context.Cards.Add(new Card() { Name = "Sales Orders" });
            if (context.Cards.SingleOrDefault(x => x.Name == "Bookings") == null)
                context.Cards.Add(new Card() { Name = "Bookings" });
            if (context.Cards.SingleOrDefault(x => x.Name == "Shipments") == null)
                context.Cards.Add(new Card() { Name = "Shipments" });
            if (context.Cards.SingleOrDefault(x => x.Name == "Inventory") == null)
                context.Cards.Add(new Card() { Name = "Inventory" });
            context.SaveChanges();
        }
    }
    internal class DashboardConfiguration
    {
        public static void Seed(AppDbContext context)
        {
            foreach (var profileId in context.Profiles.Select(x => x.ProfileId))
            {
                if (context.Dashboards.SingleOrDefault(x => x.Name == "Default" && x.ProfileId == profileId) == null)
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
        public static void Seed(AppDbContext context, IConfiguration configuration)
        {
            var index = 0;
            foreach (var username in configuration["Seed:DefaultUser:Username"].Split(','))
            {
                User user = default;
                if (context.Users.SingleOrDefault(x => x.Username == username) == null)
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
            if (context.Warehouses.SingleOrDefault(x => x.Name == "DefaultWarehouse") == null)
                context.Warehouses.Add(new Warehouse() { Name = "DefaultWarehouse" });
            context.SaveChanges();
        }
    }
    internal class StaticFileLocator
    {
        public static string GetAsString(string name)
        {
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
                    catch
                    {

                    }
                }
            }
            if (fullName == default && assembly == default)
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
                throw;
            }
        }
        public static byte[] Get(string name)
        {
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
                    catch
                    {
                    }
                }
            }
            if (fullName == default && assembly == default)
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
                throw;
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
            throw;
        }
    }
}
