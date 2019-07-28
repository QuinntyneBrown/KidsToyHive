using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems
{
    public class RemoveShipmentItem
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShipmentItemId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid ShipmentItemId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var shipmentItem = await _context.ShipmentItems.FindAsync(request.ShipmentItemId);

                _context.ShipmentItems.Remove(shipmentItem);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
