using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Customers
{
    public class UpsertCustomerTests
    {
        [Fact]
        public async Task ShouldUpsertCustomer()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(UpsertCustomerTests)}:{nameof(ShouldUpsertCustomer)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var upsertCustomerHandler = new UpsertCustomer.Handler(context);
            }
        }
    }
}
