using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.BookingDetails;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.BookingDetailId).NotNull();
    }
}
public class RemoveBookingDetailRequest : IRequest
{
    public Guid BookingDetailId { get; set; }
}
public class RemoveBookingDetailHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveBookingDetailHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveBookingDetailRequest request, CancellationToken cancellationToken)
    {
        var bookingDetail = await _context.BookingDetails.FindAsync(request.BookingDetailId);
        _context.BookingDetails.Remove(bookingDetail);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
