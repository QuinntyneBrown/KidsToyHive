using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class GetShipments
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ShipmentDto> Shipments { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    Shipments = await _context.Shipments.Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
