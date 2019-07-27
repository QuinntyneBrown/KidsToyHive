using KidsToyHive.Api;
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
                Assert.Equal(13, context.Taxes.Single().Rate);
            }
        }


    }
}