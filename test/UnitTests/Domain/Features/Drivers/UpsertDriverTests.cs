using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Drivers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Drivers
{
    public class UpsertDriverTests
    {
        [Fact]
        public async Task ShouldUpsertDriver()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(UpsertDriverTests)}:{nameof(ShouldUpsertDriver)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var upsertDriverHandler = new UpsertDriver.Handler(context);
            }
        }
    }
}
