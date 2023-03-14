using KidsToyHive.Api;
using KidsToyHive.Core.Enums;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Shipments;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Shipments;

public class GetCommittedShipmentsTests
{
    [Fact]
    public async Task ShouldGetCommittedShipmentsEmpty()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(GetCommittedShipmentsTests)}:{nameof(ShouldGetCommittedShipmentsEmpty)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var user = await context.Users.FirstAsync();
            var getCommittedShipmentsHandler = new GetCommittedShipmentsHandler(context);
            var result = await getCommittedShipmentsHandler.Handle(new GetCommittedShipmentsRequest
            {
                CurrentUsername = user.Username
            }, default);
            Assert.Empty(result.Shipments);
        }
    }
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
            var driver = await context.Drivers.FirstAsync();
            context.Shipments.Add(new Shipment
            {
                Status = ShipmentStatus.Committed
            });
            context.Shipments.Add(new Shipment
            {
                DriverId = driver.DriverId,
                Status = ShipmentStatus.Committed
            });
            context.Shipments.Add(new Shipment
            {
                DriverId = driver.DriverId,
                Status = ShipmentStatus.Completed
            });
            context.SaveChanges();
            var user = await context.Users.FirstAsync();
            var getCommittedShipmentsHandler = new GetCommittedShipmentsHandler(context);
            var result = await getCommittedShipmentsHandler.Handle(new GetCommittedShipmentsRequest
            {
                CurrentUsername = user.Username
            }, default);
            Assert.Single(result.Shipments);
        }
    }
}
