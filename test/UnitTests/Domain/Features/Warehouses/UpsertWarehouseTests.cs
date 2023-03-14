using KidsToyHive.Api;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Warehouses;

public class UpsertWarehouseTests
{
    [Fact]
    public async Task ShouldUpsertWarehouse()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(UpsertWarehouseTests)}:{nameof(ShouldUpsertWarehouse)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var upsertWarehouseHandler = new UpsertWarehouse.Handler(context);
        }
    }
}
