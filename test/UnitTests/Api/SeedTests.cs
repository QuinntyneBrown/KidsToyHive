using KidsToyHive.Api;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Api
{
    public class SeedTests
    {
        [Fact]
        public async Task ShouldSeed()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(SeedTests)}:{nameof(ShouldSeed)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("Seed:DefaultProfile:Name", "Name"),
                                new KeyValuePair<string, string>("Seed:DefaultUser:Password", ""),
                                new KeyValuePair<string, string>("Seed:DefaultUser:Username", "Username")
                })
                .Build();

            using (var context = new AppDbContext(options, mediator))
            {
                SeedData.Seed(context, configuration);

                Assert.Equal("Username", context.Users.Single().Username);
                Assert.Single(context.Users.Include(x => x.Profiles).Single().Profiles.Where(x => x.Type == ProfileType.Driver));
                Assert.Equal(.13, context.Taxes.Single().Rate);
                Assert.NotNull(context.Products.Single(x => x.Name == "Jungle Jumparoo"));
                Assert.NotNull(context.Warehouses.Single(x => x.Name == "DefaultWarehouse"));
                Assert.NotNull(context.InventoryItems.Include(x => x.Product).Single(x => x.Product.Name == "Jungle Jumparoo"));
                Assert.Equal(3,context.DigitalAssets.Where(x => x.Name.Contains("Hero")).Count());
            }
        }


    }
}
