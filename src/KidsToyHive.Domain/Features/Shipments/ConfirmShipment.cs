using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class ConfirmShipment
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(x => x.ShipmentId).NotNull();
                RuleFor(x => x.SignatureId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public Guid ShipmentId { get; set; }
            public Guid SignatureId { get; set; }
        }

        public class Response
        {
            public Guid ShipmentId { get; set; }
            public int Version { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var shipment = _context.Shipments.Find(request.ShipmentId);

                shipment.SignatureId = request.SignatureId;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() {
                    ShipmentId = shipment.ShipmentId,
                    Version = shipment.Version
                };
            }
        }
    }
}
