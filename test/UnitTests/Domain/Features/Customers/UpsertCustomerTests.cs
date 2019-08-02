using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Features.Addresses;
using KidsToyHive.Domain.Features.Customers;
using KidsToyHive.Domain.Features.Locations;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
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
            var emailSender = new Mock<IEmailService>().Object;
            var passwordHasher = new Mock<IPasswordHasher>().Object;
            var securityTokenFactory = new Mock<ISecurityTokenFactory>().Object;

            using (var context = new AppDbContext(options, mediator))
            {
                var upsertCustomerHandler = new UpsertCustomer.Handler(context,passwordHasher,emailSender,securityTokenFactory);

                var address = new AddressDto
                {
                    City = "Toronto",
                    Province = "Ontario",
                    PostalCode = "M5V 1A8"
                };

                var customer = new CustomerDto
                {
                    FirstName = "Quinntyne",
                    LastName = "Brown",
                    Email = "quinntynebrown@gmail.com",
                    PhoneNumber = "416 967 1111",
                    Address = address
                };

                customer.CustomerLocations.Add(new CustomerLocationDto
                {
                    Location = new LocationDto {
                        Type = LocationType.DeliveryPickUp,
                        Address = address
                    }
                });

                var result = await upsertCustomerHandler.Handle(new UpsertCustomer.Request {
                    Customer = customer
                }, default);

                var persistedCustomer = context.Customers
                    .Include(x => x.Address)
                    .Include(x => x.CustomerLocations)
                    .ThenInclude(x => x.Location)
                    .ThenInclude(x => x.Adddress)
                    .First();

                Assert.Single(persistedCustomer.CustomerLocations);
                Assert.Equal(1, persistedCustomer.Version);

            }
        }
    }
}
