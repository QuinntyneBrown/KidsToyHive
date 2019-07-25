using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentBookings
{
    public class UpsertShipmentBooking
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ShipmentBooking).NotNull();
                RuleFor(request => request.ShipmentBooking).SetValidator(new ShipmentBookingDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public ShipmentBookingDto ShipmentBooking { get; set; }
        }

        public class Response
        {
            public Guid ShipmentBookingId { get;set; }
            public int Version { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var shipmentBooking = await _context.ShipmentBookings.FindAsync(request.ShipmentBooking.ShipmentBookingId);

                if (shipmentBooking == null) {
                    shipmentBooking = new ShipmentBooking();
                    _context.ShipmentBookings.Add(shipmentBooking);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() {
                    ShipmentBookingId = shipmentBooking.ShipmentBookingId,
                    Version = shipmentBooking.Version
                };
            }
        }
    }
}
