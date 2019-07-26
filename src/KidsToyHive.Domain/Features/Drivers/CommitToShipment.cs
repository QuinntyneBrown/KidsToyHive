using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers
{
    public class CommitToShipment
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {
            public Guid DriverId { get; set; }
            public Guid ShipmentId { get; set; }
        }

        public class Response
        {

        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var driver = await _context.Drivers.FindAsync(request.DriverId);

                var shipment = await _context.Shipments.FindAsync(request.ShipmentId);

                driver.Shipments.Add(shipment);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { };
            }
        }
    }
}
