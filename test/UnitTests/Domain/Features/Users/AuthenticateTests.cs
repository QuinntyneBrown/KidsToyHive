using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Identity;
using KidsToyHive.Infrastructure.Data;
using KidsToyHive.Domain.Features.BookingDetails;
using KidsToyHive.Domain.Features.Bookings;
using KidsToyHive.Domain.Features.Products;
using KidsToyHive.Domain.Features.Users;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Domain.Features.Users;

public class AuthenticateTests
{
    [Fact]
    public async Task ShouldAuthenticate()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"{nameof(AuthenticateTests)}:{nameof(ShouldAuthenticate)}")
            .Options;
        var mediator = new Mock<IMediator>().Object;
        var mockSecurityTokenFactory = new Mock<ISecurityTokenFactory>();
        mockSecurityTokenFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<List<Claim>>())).Returns("token");
        var securityTokenFactory = mockSecurityTokenFactory.Object;
        var passwordHasher = new PasswordHasher();
        using (var context = new AppDbContext(options, mediator))
        {
            var user = new User { Username = "foo" };
            user.Password = new PasswordHasher().HashPassword(user.Salt, "bar");
            context.Users.Add(user);
            context.SaveChanges();
            var authenticateHandler = new AuthenticateHandler(context, securityTokenFactory, passwordHasher);
            var result = await authenticateHandler.Handle(new AuthenticateRequest
            {
                Username = "foo",
                Password = "bar"
            }, default);
            Assert.Equal("token", result.AccessToken);
        }
    }
}
