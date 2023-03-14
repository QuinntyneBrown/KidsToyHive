using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Warehouses;

public class GetWarehouseById
{
    public class Request : IRequest<Response>
    {
        public Guid WarehouseId { get; set; }
    }
    public class Response
    {
        public WarehouseDto Warehouse { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                Warehouse = (await _context.Warehouses.FindAsync(request.WarehouseId)).ToDto()
            };
    }
}
