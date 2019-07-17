using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Customers
{
    public class RemoveCustomer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CustomerId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.FindAsync(request.CustomerId);

                _context.Customers.Remove(customer);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
