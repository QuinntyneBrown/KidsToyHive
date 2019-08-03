using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Identity;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Models.DomainEvents;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Customers
{
    public class UpsertCustomer
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Customer).NotNull();
                RuleFor(request => request.Customer).SetValidator(new CustomerDtoValidator());                
            }
        }

        [AllowAnonymous]
        public class Request : Command<Response> {
            public CustomerDto Customer { get; set; }
            public override IEnumerable<string> SideEffects => new string[] { "Customer" };
        }

        public class Response
        {
            public Guid CustomerId { get;set; }
            public int Version { get; set; }
            public string AccessToken { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IEmailService _emailService;
            private readonly ISecurityTokenFactory _securityTokenFactory;
            public Handler(
                IAppDbContext context, 
                IPasswordHasher passwordHasher,
                IEmailService emailService,
                ISecurityTokenFactory securityTokenFactory)
            {
                _context = context;
                _emailService = emailService;
                _passwordHasher = passwordHasher;
                _securityTokenFactory = securityTokenFactory;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var customer = await _context.Customers.FindAsync(request.Customer.CustomerId);

                if (customer == null) {
                    if (await _context.Customers.AnyAsync(x => x.Email == request.Customer.Email))
                        throw new Exception();

                    customer = new Customer();
                    customer.RaiseDomainEvent(new CustomerCreated(customer));
                    _context.Customers.Add(customer);
                }

                customer.FirstName = request.Customer.FirstName;
                customer.LastName = request.Customer.LastName;
                customer.PhoneNumber = request.Customer.PhoneNumber;
                customer.Email = request.Customer.Email;

                if(request.Customer.Address != null)
                    customer.Address = new Address(
                        request.Customer.Address.Street,
                        request.Customer.Address.City,
                        request.Customer.Address.Province,
                        request.Customer.Address.PostalCode);

                customer.CustomerLocations.Clear();

                foreach(var customerLocationDto in request.Customer.CustomerLocations)
                {
                    var customerLocation = await _context.CustomerLocations.FindAsync(customerLocationDto.CustomerLocationId);

                    if(customerLocation == null)
                        customerLocation = new CustomerLocation();

                    customerLocation.Location = await _context.Locations.FindAsync(customerLocationDto.LocationId);

                    if (customerLocation.Location == null)
                        customerLocation.Location = new Location();

                    customerLocation.Name = customerLocationDto.Name;
                    customerLocation.Location = new Location();
                    customerLocation.LocationId = customerLocationDto.LocationId;
                    customerLocation.Location.Type = customerLocationDto.Location.Type;
                    customerLocation.Location.Adddress = new Address(
                        customerLocationDto.Location.Address.Street,
                        customerLocationDto.Location.Address.City,
                        customerLocationDto.Location.Address.Province,
                        customerLocationDto.Location.Address.PostalCode);

                    customer.CustomerLocations.Add(customerLocation);
                }

                await _context.SaveChangesAsync(cancellationToken);

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == customer.Email);

                if (user == null)
                {
                    user = new User
                    {
                        Username = customer.Email
                    };

                    user.Password = _passwordHasher.HashPassword(user.Salt, "P@ssw0rd");

                    await _context.Users.AddAsync(user);

                    user.Profiles.Add(new Profile
                    {
                        Name = $"{customer.FirstName} {customer.LastName}",
                        Type = ProfileType.Customer
                    });

                    await _context.Users.AddAsync(user);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                if (request.Customer.CustomerId == default)
                    _emailService.SendNewCustomer(customer, user);

                return new Response() {
                    CustomerId = customer.CustomerId,
                    Version = customer.Version,
                    AccessToken = _securityTokenFactory.Create(user.Username,new List<Claim>() {
                        new Claim("UserId", $"{user.UserId}"),
                        new Claim("PartitionKey", $"{user.TenantKey}"),
                        new Claim($"{nameof(customer.CustomerId)}",$"{customer.CustomerId}"),
                        new Claim(ClaimTypes.Role, nameof(ProfileType.Customer)),
                        new Claim("CurrentUserName",$"{user.Username}")
                    })
                };
            }
        }
    }
}
