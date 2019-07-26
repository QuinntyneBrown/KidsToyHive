using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers
{
    public class GetDriverById
    {
        public class Request : IRequest<Response> {
            public Guid DriverId { get; set; }
        }

        public class Response
        {
            public DriverDto Driver { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Driver = (await _context.Drivers.FindAsync(request.DriverId)).ToDto()
                };
        }
    }
}
