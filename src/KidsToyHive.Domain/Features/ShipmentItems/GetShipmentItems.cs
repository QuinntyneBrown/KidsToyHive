using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems
{
    public class GetShipmentItems
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ShipmentItemDto> ShipmentItems { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    ShipmentItems = await _context.ShipmentItems.Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
