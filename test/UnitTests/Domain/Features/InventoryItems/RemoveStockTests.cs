using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.InventoryItems;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.InventoryItems;

public class RemoveStockTests
{
    [Fact]
    public async Task ShouldRemoveStock()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(RemoveStockTests)}:{nameof(ShouldRemoveStock)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            var removeStockHandler = new RemoveStockHandler(context);
        }
    }
}
