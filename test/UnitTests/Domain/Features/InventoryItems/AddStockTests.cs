using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.InventoryItems;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.InventoryItems
{
    public class AddStockTests
    {
        [Fact]
        public async Task ShouldAddStock()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(AddStockTests)}:{nameof(ShouldAddStock)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var addStockHandler = new AddStock.Handler(context);

                var result = await addStockHandler.Handle(new AddStock.Request { }, default);

                Assert.NotNull(result);
            }
        }
    }
}
