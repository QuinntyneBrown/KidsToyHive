using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins
{
    public class GetBinById
    {
        public class Request : IRequest<Response> {
            public Guid BinId { get; set; }
        }

        public class Response
        {
            public BinDto Bin { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Bin = (await _context.Bins.FindAsync(request.BinId)).ToDto()
                };
        }
    }
}
