using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.BookingDetails
{
    public class GetBookingDetailById
    {
        public class Request : IRequest<Response> {
            public Guid BookingDetailId { get; set; }
        }

        public class Response
        {
            public BookingDetailDto BookingDetail { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    BookingDetail = (await _context.BookingDetails.FindAsync(request.BookingDetailId)).ToDto()
                };
        }
    }
}
