using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class RemoveShipment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShipmentId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid ShipmentId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var shipment = await _context.Shipments.FindAsync(request.ShipmentId);

                _context.Shipments.Remove(shipment);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
