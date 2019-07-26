using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.BookingDetails;
using KidsToyHive.Domain.Features.Bookings;
using KidsToyHive.Domain.Features.Products;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Scenarios.Bookings
{
    public class BookingScenarios
    {
        [Fact]
        public async Task ShouldUpsertBooking()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(BookingScenarios)}:{nameof(ShouldUpsertBooking)}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var productId = Guid.NewGuid();
                var mockInventoryService = new Mock<IInventoryService>();
                mockInventoryService.Setup(x => x.IsItemAvailable(It.IsAny<DateTime>(), It.IsAny<BookingTimeSlot>(), It.IsAny<Guid>())).Returns(true);
                var inventoryService = mockInventoryService.Object;

                var mediator = new Mock<IMediator>().Object;
                var upsertBookingHandler = new UpsertBooking.Handler(context, mediator, inventoryService);
                var booking = new BookingDto
                {
                    BookingDetails = new List<BookingDetailDto>
                    {
                        new BookingDetailDto {
                            Quantity = 1, Product = new ProductDto { ProductId = productId, HourlyRate = 31.25f }
                        }
                    }
                };

                var request = new UpsertBooking.Request
                {
                    Booking = booking
                };

                var response = await upsertBookingHandler.Handle(request, default);

                Assert.NotNull(response);
                Assert.Equal(1, response.Version);
                Assert.NotEqual(default, response.BookingId);
            }
            
        }
    }
}
