// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Api;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.Bookings;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Bookings;

public class GetMyBookingsTests
{
    [Fact]
    public async Task ShouldGetMyBookings()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(GetMyBookingsTests)}:{nameof(ShouldGetMyBookings)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var user = context.Users.First();
            var customer = new Customer
            {
                Email = user.Username
            };
            context.Customers.Add(customer);
            context.Bookings.Add(new Booking
            {
                Customer = customer
            });
            context.Bookings.Add(new Booking
            {
            });
            context.SaveChanges();
            var getMyBookingsHandler = new GetMyBookingsHandler(context);
            var result = await getMyBookingsHandler.Handle(new GetMyBookingsRequest
            {
                CurrentUsername = user.Username
            }, default);
            Assert.Single(result.Bookings);
        }
    }

    [Fact]
    public async Task ShouldGetMyBookingsEmpty()
    {
        var options = new DbContextOptionsBuilder<KidsToyHiveDbContext>()
            .UseInMemoryDatabase($"{nameof(GetMyBookingsTests)}:{nameof(ShouldGetMyBookingsEmpty)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        using (var context = new AppDbContext(options, mediator))
        {
            SeedData.Seed(context, ConfigurationHelper.Seed);
            var user = context.Users.First();
            context.Customers.Add(new Customer
            {
                Email = user.Username
            });
            context.SaveChanges();
            var getMyBookingsHandler = new GetMyBookingsHandler(context);
            var result = await getMyBookingsHandler.Handle(new GetMyBookingsRequest
            {
                CurrentUsername = user.Username
            }, default);
            Assert.Empty(result.Bookings);
        }
    }
}

