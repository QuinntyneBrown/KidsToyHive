using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Bookings;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Bookings
{
    public class ProcessBookingPaymentTests
    {
        [Fact]
        public async Task ShouldProcessBookingPayment()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(ProcessBookingPaymentTests)}:{nameof(ShouldProcessBookingPayment)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var mockPaymentProcessor = new Mock<IPaymentProcessor>();

                var processBookingPaymentHandler = new CheckoutBooking.Handler(context,mockPaymentProcessor.Object);
            }
        }
    }
}
