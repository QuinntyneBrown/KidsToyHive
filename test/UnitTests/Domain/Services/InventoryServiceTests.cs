using KidsToyHive.Api;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Services;

public class InventoryServiceTests
{
    [Fact]
    public async Task ShouldReturnNotAvailableIfBookingsOnSameDateAndTimeSlot()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(InventoryServiceTests)}:{nameof(ShouldReturnNotAvailableIfBookingsOnSameDateAndTimeSlot)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var product = context.Products.First();
            var date = new DateTime(2019, 8, 6);
            var booking = new Booking
            {
                Date = date,
                BookingTimeSlot = BookingTimeSlot.Morning
            };
            booking.BookingDetails.Add(new BookingDetail
            {
                ProductId = product.ProductId
            });
            context.Bookings.Add(booking);
            context.SaveChanges();
            IInventoryService inventoryService = new InventoryService(context);
            var result = await inventoryService.IsItemAvailable(date, BookingTimeSlot.Morning, product.ProductId);
            Assert.False(result);
        }
    }
    [Fact]
    public async Task ShouldReturnAvailableIfBookingsOnSameDateAndDifferentTimeSlot()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(InventoryServiceTests)}:{nameof(ShouldReturnAvailableIfBookingsOnSameDateAndDifferentTimeSlot)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var product = context.Products.First();
            var date = new DateTime(2019, 8, 6);
            var booking = new Booking
            {
                Date = date,
                BookingTimeSlot = BookingTimeSlot.Morning
            };
            booking.BookingDetails.Add(new BookingDetail
            {
                ProductId = product.ProductId
            });
            context.Bookings.Add(booking);
            context.SaveChanges();
            IInventoryService inventoryService = new InventoryService(context);
            var result = await inventoryService.IsItemAvailable(date, BookingTimeSlot.Afternoon, product.ProductId);
            Assert.True(result);
        }
    }
}
