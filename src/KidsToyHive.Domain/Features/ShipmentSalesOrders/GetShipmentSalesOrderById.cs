using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentSalesOrders
{
    public class GetShipmentSalesOrderById
    {
        public class Request : IRequest<Response> {
            public Guid ShipmentSalesOrderId { get; set; }
        }

        public class Response
        {
            public ShipmentSalesOrderDto ShipmentSalesOrder { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    ShipmentSalesOrder = (await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrderId)).ToDto()
                };
        }
    }
}
