using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders;

public class GetSalesOrdersRequest : IRequest<GetSalesOrdersResponse> { }
public class GetSalesOrdersResponse
{
    public IEnumerable<SalesOrderDto> SalesOrders { get; set; }
}
public class GetSalesOrdersHandler : IRequestHandler<GetSalesOrdersRequest, GetSalesOrdersResponse>
{
    private readonly IAppDbContext _context;
    public GetSalesOrdersHandler(IAppDbContext context) => _context = context;
    public async Task<GetSalesOrdersResponse> Handle(GetSalesOrdersRequest request, CancellationToken cancellationToken)
        => new GetSalesOrdersResponse()
        {
            SalesOrders = await _context.SalesOrders.Select(x => x.ToDto()).ToArrayAsync()
        };
}
