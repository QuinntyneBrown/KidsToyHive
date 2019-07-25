using FluentValidation;
using KidsToyHive.Core.Enums;
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
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;
            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var shipment = _context.Shipments.Find(request.ShipmentId);

                shipment.SignatureId = request.SignatureId;

                await _context.SaveChangesAsync(cancellationToken);

                if (shipment.Type == ShipmentType.Delivery)
                    await _mediator.Publish(new ShipmentDelivered.Notification
                    {
                        ShipmentId = shipment.ShipmentId
                    });

                return new Response() {
                    ShipmentId = shipment.ShipmentId,
                    Version = shipment.Version
                };
            }
        }
    }
}
