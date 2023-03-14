using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentBookings;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.ShipmentBookingId).NotNull();
    }
}
public class RemoveShipmentBookingRequest : IRequest
{
    public Guid ShipmentBookingId { get; set; }
}
public class RemoveShipmentBookingHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveShipmentBookingHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveShipmentBookingRequest request, CancellationToken cancellationToken)
    {
        var shipmentBooking = await _context.ShipmentBookings.FindAsync(request.ShipmentBookingId);
        _context.ShipmentBookings.Remove(shipmentBooking);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
