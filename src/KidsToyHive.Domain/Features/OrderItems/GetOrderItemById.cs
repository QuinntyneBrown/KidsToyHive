using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.OrderItems
{
    public class GetOrderItemById
    {
        public class Request : IRequest<Response> {
            public Guid OrderItemId { get; set; }
        }

        public class Response
        {
            public OrderItemDto OrderItem { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    OrderItem = (await _context.OrderItems.FindAsync(request.OrderItemId)).ToDto()
                };
        }
    }
}
