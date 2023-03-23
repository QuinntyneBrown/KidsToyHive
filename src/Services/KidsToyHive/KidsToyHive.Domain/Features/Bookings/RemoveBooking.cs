using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

public class RemoveBookingValidator : AbstractValidator<RemoveBookingRequest>
{
    public RemoveBookingValidator()
    {
        RuleFor(request => request.BookingId).NotNull();
    }
}
public class RemoveBookingRequest : IRequest
{
    public Guid BookingId { get; set; }
}
public class RemoveBookingHandler : IRequestHandler<RemoveBookingRequest>
{
    private readonly IAppDbContext _context;
    public RemoveBookingHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveBookingRequest request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings.FindAsync(request.BookingId);
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
