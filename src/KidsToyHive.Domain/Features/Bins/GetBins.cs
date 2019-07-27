using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins
{
    public class GetBins
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<BinDto> Bins { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    Bins = await _context.Bins.Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
