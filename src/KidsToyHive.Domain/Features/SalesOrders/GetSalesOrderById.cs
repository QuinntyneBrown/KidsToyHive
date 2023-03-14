using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders;

public class GetSalesOrderByIdRequest : IRequest<GetSalesOrderByIdResponse>
{
    public Guid SalesOrderId { get; set; }
}
public class GetSalesOrderByIdResponse
{
    public SalesOrderDto SalesOrder { get; set; }
}
public class GetSalesOrderByIdHandler : IRequestHandler<GetSalesOrderByIdRequest, GetSalesOrderByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetSalesOrderByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetSalesOrderByIdResponse> Handle(GetSalesOrderByIdRequest request, CancellationToken cancellationToken)
        => new GetSalesOrderByIdResponse()
        {
            SalesOrder = (await _context.SalesOrders.FindAsync(request.SalesOrderId)).ToDto()
        };
}
