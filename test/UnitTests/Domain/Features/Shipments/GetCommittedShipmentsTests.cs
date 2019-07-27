using KidsToyHive.Api;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Shipments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Shipments
{
    public class GetCommittedShipmentsTests
    {
        [Fact]
        public async Task ShouldGetCommittedShipments()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(GetCommittedShipmentsTests)}:{nameof(ShouldGetCommittedShipments)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                SeedData.Seed(context, ConfigurationHelper.Seed);

                var user = await context.Users.FirstAsync();

                var getCommittedShipmentsHandler = new GetCommittedShipments.Handler(context);

                var result = await getCommittedShipmentsHandler.Handle(new GetCommittedShipments.Request {
                    CurrentUsername = user.Username
                }, default);

                Assert.Empty(result.Shipments);
            }
        }
    }
}
