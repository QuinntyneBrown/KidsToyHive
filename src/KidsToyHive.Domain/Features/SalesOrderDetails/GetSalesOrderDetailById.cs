using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrderDetails
{
    public class GetSalesOrderDetailById
    {
        public class Request : IRequest<Response> {
            public Guid SalesOrderDetailId { get; set; }
        }

        public class Response
        {
            public SalesOrderDetailDto SalesOrderDetail { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    SalesOrderDetail = (await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetailId)).ToDto()
                };
        }
    }
}
