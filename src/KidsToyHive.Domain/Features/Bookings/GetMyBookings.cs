using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class GetMyBookings
    {
        public class Request : AuthenticatedRequest<Response> { }

        public class Response
        {
            public ICollection<BookingDto> Bookings { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var bookings = (await _context.Customers
                    .Include(x => x.Bookings)
                    .ThenInclude(x => x.BookingDetails)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductImages)
                    .ThenInclude(x => x.DigitalAsset)
                    .SingleAsync(x => x.Email == request.CurrentUsername))
                    .Bookings
                    .Select(x => x.ToDto())
                    .ToList();

                return new Response()
                {
                    Bookings = bookings
                };
            }
        }
    }
}
