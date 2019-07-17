using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
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

        public class Request : IRequest<Response> {
            public CustomerDto Customer { get; set; }
        }

        public class Response
        {
            public Guid CustomerId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var customer = await _context.Customers.FindAsync(request.Customer.CustomerId);

                if (customer == null) {
                    customer = new Customer();
                    _context.Customers.Add(customer);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { CustomerId = customer.CustomerId };
            }
        }
    }
}
