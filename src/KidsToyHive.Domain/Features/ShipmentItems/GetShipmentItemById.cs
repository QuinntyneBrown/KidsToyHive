using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems;

public class GetShipmentItemById
{
    public class Request : IRequest<Response>
    {
        public Guid ShipmentItemId { get; set; }
    }
    public class Response
    {
        public ShipmentItemDto ShipmentItem { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                ShipmentItem = (await _context.ShipmentItems.FindAsync(request.ShipmentItemId)).ToDto()
            };
    }
}
