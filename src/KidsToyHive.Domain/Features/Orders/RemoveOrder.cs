using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Orders
{
    public class RemoveOrder
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.OrderId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.FindAsync(request.OrderId);

                _context.Orders.Remove(order);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
