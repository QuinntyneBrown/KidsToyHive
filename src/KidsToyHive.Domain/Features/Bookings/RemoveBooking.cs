using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

public class RemoveBooking
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.BookingId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid BookingId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FindAsync(request.BookingId);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
