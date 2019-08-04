using KidsToyHive.Api;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Addresses;
using KidsToyHive.Domain.Features.BookingDetails;
using KidsToyHive.Domain.Features.Bookings;
using KidsToyHive.Domain.Features.Locations;
using KidsToyHive.Domain.Features.Products;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Bookings
{
    public class UpsertBookingTests
    {
        [Fact]
        public async Task ShouldUpsertBooking()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"{nameof(UpsertBookingTests)}:{nameof(ShouldUpsertBooking)}")
                .Options;

            var mediator = new Mock<IMediator>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                SeedData.Seed(context, ConfigurationHelper.Seed);
                var customer = new Customer();
                context.Customers.Add(customer);
                context.SaveChanges();

                var product = context.Products.First();
                var mockInventoryService = new Mock<IInventoryService>();
                mockInventoryService.Setup(x => x.IsItemAvailable(It.IsAny<DateTime>(), It.IsAny<BookingTimeSlot>(), It.IsAny<Guid>())).Returns(true);
                var inventoryService = mockInventoryService.Object;
                var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
                var mockHttpContext = new Mock<HttpContext>();
                var mockUser = new Mock<ClaimsPrincipal>();

                mockUser.Setup(x => x.Claims).Returns(new List<Claim>()
                {
                    new Claim("CustomerId",$"{customer.CustomerId}")
                });

                mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
                mockHttpContext.Setup(x => x.User).Returns(mockUser.Object);
                
                var upsertBookingHandler = new UpsertBooking.Handler(context, inventoryService, mockHttpContextAccessor.Object);

                var booking = new BookingDto
                {
                    BookingDetails = new List<BookingDetailDto> {
                        new BookingDetailDto {
                            Quantity = 1,
                            ProductId = product.ProductId,
                            Product = new ProductDto {
                                ProductId = product.ProductId,
                                ChargePeriodPrice = product.ChargePeriodPrice
                            }
                        }
                    },
                    Location = new LocationDto
                    {
                        Address = new AddressDto
                        {
                            Street = "628 Fleet Street",
                            City = "Toronto",
                            Province = "Ontario",
                            PostalCode = "M5V 1A8"
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
