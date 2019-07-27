using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class GetCommittedShipments
    {
        public class Request : AuthenticatedRequest<Response> {
            
        }

        public class Response
        {
            public ICollection<ShipmentDto> Shipments { get; set; }
            = new HashSet<ShipmentDto>();
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) { 
            
                var driver = await _context.Drivers.SingleAsync(x => x.Email == request.CurrentUsername);

                return new Response()
                {
                    Shipments = _context.Shipments
                    .Where(x => x.DriverId == driver.DriverId
                    && x.Status == ShipmentStatus.Committed)
                    .Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
