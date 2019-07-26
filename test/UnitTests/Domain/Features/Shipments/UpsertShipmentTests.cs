using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Shipments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Shipments
{
    public class UpsertShipmentTests
    {
        [Fact]
        public async Task ShouldUpsertShipment()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(UpsertShipmentTests)}:{nameof(ShouldUpsertShipment)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var upsertShipmentHandler = new UpsertShipment.Handler(context);
            }
        }
    }
}
