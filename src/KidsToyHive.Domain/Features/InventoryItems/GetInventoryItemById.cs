using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems
{
    public class GetInventoryItemById
    {
        public class Request : IRequest<Response> {
            public Guid InventoryItemId { get; set; }
        }

        public class Response
        {
            public InventoryItemDto InventoryItem { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    InventoryItem = (await _context.InventoryItems.FindAsync(request.InventoryItemId)).ToDto()
                };
        }
    }
}
