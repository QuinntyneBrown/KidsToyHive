using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders;

public class GetSalesOrderById
{
    public class Request : IRequest<Response>
    {
        public Guid SalesOrderId { get; set; }
    }
    public class Response
    {
        public SalesOrderDto SalesOrder { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                SalesOrder = (await _context.SalesOrders.FindAsync(request.SalesOrderId)).ToDto()
            };
    }
}
