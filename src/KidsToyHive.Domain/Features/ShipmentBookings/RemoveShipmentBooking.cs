using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentBookings;

public class RemoveShipmentBooking
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.ShipmentBookingId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid ShipmentBookingId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var shipmentBooking = await _context.ShipmentBookings.FindAsync(request.ShipmentBookingId);
            _context.ShipmentBookings.Remove(shipmentBooking);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
