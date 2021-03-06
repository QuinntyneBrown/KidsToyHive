using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.SalesOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.SalesOrders
{
    public class UpsertSalesOrderTests
    {
        [Fact]
        public async Task ShouldUpsertSalesOrder()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(UpsertSalesOrderTests)}:{nameof(ShouldUpsertSalesOrder)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var upsertSalesOrderHandler = new UpsertSalesOrder.Handler(context);
            }
        }
    }
}
