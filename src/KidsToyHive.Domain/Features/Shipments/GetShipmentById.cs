using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments;

public class GetShipmentById
{
    public class Request : IRequest<Response>
    {
        public Guid ShipmentId { get; set; }
    }
    public class Response
    {
        public ShipmentDto Shipment { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                Shipment = (await _context.Shipments.FindAsync(request.ShipmentId)).ToDto()
            };
    }
}
