using KidsToyHive.Api;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Shipments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Shipments;

public class GetIncompleteShipmentsTests
{
    [Fact]
    public async Task ShouldGetIncompleteShipments()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(GetIncompleteShipmentsTests)}:{nameof(ShouldGetIncompleteShipments)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var getIncompleteShipmentsHandler = new GetIncompleteShipments.Handler(context);
            var result = await getIncompleteShipmentsHandler.Handle(new GetIncompleteShipments.Request { }, default);
            Assert.Empty(result.Shipments);
        }
    }
}
