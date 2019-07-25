using FluentValidation;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public class Request : Command<Response>
        {
            public BookingDto Booking { get; set; }

            public override string Key => Build("Booking", $"{Booking.BookingId}", $"{Booking.Version}");

            public override IEnumerable<string> SideEffects
            {
                get
                {
                    var sideEffects = new List<string>();

                    sideEffects.AddRange(Booking.BookingDetails
                        .Select(x => $"Product {x.ProductId}"));

                    return sideEffects;
                }
            }
        }

        public class Response
        {
            public Guid BookingId { get;set; }
            public int Version { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;
            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var booking = await _context.Bookings.FindAsync(request.Booking.BookingId);

                bool newBooking = booking == null;

                if (newBooking) {
                    booking = new Booking();
                    _context.Bookings.Add(booking);
                }

                booking.BookingDetails.Clear();

                if (_context.Bookings.Any(x => x.Date == request.Booking.Date && x.BookingTimeSlot == request.Booking.BookingTimeSlot))
                    throw new Exception();

                foreach(var bookingDetail in request.Booking.BookingDetails)
                {
                    booking.BookingDetails.Add(new BookingDetail
                    {
                        ProductId = bookingDetail.ProductId,
                        Quantity = bookingDetail.Quantity
                    });
                }

                booking.Cost = 125;

                await _context.SaveChangesAsync(cancellationToken);

                if(newBooking)
                    await _mediator.Publish(new BookingCreated.Notification()
                    {
                        BookingId = booking.BookingId
                    });

                return new Response() {
                    BookingId = booking.BookingId,
                    Version = booking.Version
                };
            }
        }
    }
}
