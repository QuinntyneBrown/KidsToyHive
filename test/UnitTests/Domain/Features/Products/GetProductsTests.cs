using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Products
{
    public class GetProductsTests
    {
        [Fact]
        public async Task ShouldGetProducts()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(GetProductsTests)}:{nameof(ShouldGetProducts)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var getProductsHandler = new GetProducts.Handler(context);
            }
        }
    }
}
