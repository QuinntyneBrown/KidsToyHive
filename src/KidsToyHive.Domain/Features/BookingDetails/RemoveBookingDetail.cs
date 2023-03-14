using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.BookingDetails;

public class RemoveBookingDetail
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.BookingDetailId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid BookingDetailId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var bookingDetail = await _context.BookingDetails.FindAsync(request.BookingDetailId);
            _context.BookingDetails.Remove(bookingDetail);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
