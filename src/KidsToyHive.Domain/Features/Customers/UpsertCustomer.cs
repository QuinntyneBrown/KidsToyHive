using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using KidsToyHive.Domain.Models.DomainEvents;

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

        public class Request : IRequest<Response> {
            public CustomerDto Customer { get; set; }
        }

        public class Response
        {
            public Guid CustomerId { get;set; }
            public int Version { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var customer = await _context.Customers.FindAsync(request.Customer.CustomerId);

                if (customer == null) {
                    customer = new Customer();
                    customer.RaiseDomainEvent(new CustomerCreated(customer));
                    _context.Customers.Add(customer);
                }

                customer.FirstName = request.Customer.FirstName;
                customer.LastName = request.Customer.LastName;
                customer.PhoneNumber = request.Customer.PhoneNumber;
                customer.Email = request.Customer.Email;
                customer.Address = new Address(
                    request.Customer.Address.Street,
                    request.Customer.Address.City,
                    request.Customer.Address.Province,
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
                        customerLocationDto.Location.Address.Province,
                        customerLocationDto.Location.Address.PostalCode);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() {
                    CustomerId = customer.CustomerId,
                    Version = customer.Version
                };
            }
        }
    }
}
