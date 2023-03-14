using KidsToyHive.Api;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Fakes;
using KidsToyHive.Domain.Features.Bookings;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Bookings;

public class ProcessBookingPaymentTests
{
    [Fact]
    public async Task ShouldProcessBookingPayment()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(ProcessBookingPaymentTests)}:{nameof(ShouldProcessBookingPayment)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        var emailService = new Mock<IEmailService>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var product = context.Products.Single();
            var booking = new Booking();

            booking.BookingDetails.Add(new BookingDetail
            {
                Cost = 4 * product.ChargePeriodPrice
            });
            context.Bookings.Add(booking);
            context.SaveChanges();
            Assert.Equal(12500, booking.Cost);
            var processBookingPaymentHandler = new CheckoutBookingHandler(context, emailService, new FakePaymentProcessor());
            var result = await processBookingPaymentHandler.Handle(new CheckoutBookingRequest
            {
                BookingId = booking.BookingId,
                Number = "",
                Cvc = "",
                ExpYear = 09,
                ExpMonth = 22,
            }, default);
        }
    }
}
