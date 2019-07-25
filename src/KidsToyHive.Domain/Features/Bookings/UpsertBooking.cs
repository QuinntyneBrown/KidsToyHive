using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class UpsertBooking
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Booking).NotNull();
                RuleFor(request => request.Booking).SetValidator(new BookingDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public BookingDto Booking { get; set; }
        }

        public class Response
        {
            public Guid BookingId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var booking = await _context.Bookings.FindAsync(request.Booking.BookingId);

                if (booking == null) {
                    booking = new Booking();
                    _context.Bookings.Add(booking);
                }

                booking.Name = request.Booking.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { BookingId = booking.BookingId };
            }
        }
    }
}
