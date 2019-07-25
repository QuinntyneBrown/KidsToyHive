using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class GetBookingById
    {
        public class Request : IRequest<Response> {
            public Guid BookingId { get; set; }
        }

        public class Response
        {
            public BookingDto Booking { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Booking = (await _context.Bookings.FindAsync(request.BookingId)).ToDto()
                };
        }
    }
}
