using KidsToyHive.Api;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Bookings;
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

namespace UnitTests.Domain.Features.Bookings;

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
            var product = context.Products.First();
            var inventoryService = new InventoryService(context);
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockUser = new Mock<ClaimsPrincipal>();
            mockUser.Setup(x => x.Claims).Returns(new List<Claim>()
             {
                 new Claim("CustomerId",$"{Guid.NewGuid()}")
             });
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            mockHttpContext.Setup(x => x.User).Returns(mockUser.Object);

            var upsertBookingHandler = new UpsertBookingHandler(context, inventoryService, mockHttpContextAccessor.Object);
            var request = new UpsertBookingRequest
            {
                Booking = BookingBuilder.Build(BookingTimeSlot.Afternoon, DateTime.Now, product)
            };
            var response = await upsertBookingHandler.Handle(request, default);
            Assert.NotNull(response);
            Assert.Equal(1, response.Version);
            Assert.NotEqual(default, response.BookingId);
        }
    }
}
