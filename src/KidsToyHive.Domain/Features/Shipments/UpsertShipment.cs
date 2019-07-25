using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class UpsertShipment
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Shipment).NotNull();
                RuleFor(request => request.Shipment).SetValidator(new ShipmentDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public ShipmentDto Shipment { get; set; }
        }

        public class Response
        {
            public Guid ShipmentId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var shipment = await _context.Shipments.FindAsync(request.Shipment.ShipmentId);

                if (shipment == null) {
                    shipment = new Shipment();
                    _context.Shipments.Add(shipment);
                }

                shipment.Name = request.Shipment.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ShipmentId = shipment.ShipmentId };
            }
        }
    }
}
