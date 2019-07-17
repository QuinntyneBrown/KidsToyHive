using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.OrderItems
{
    public class RemoveOrderItem
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.OrderItemId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid OrderItemId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var orderItem = await _context.OrderItems.FindAsync(request.OrderItemId);

                _context.OrderItems.Remove(orderItem);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
