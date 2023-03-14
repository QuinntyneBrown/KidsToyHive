using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentSalesOrders;

public class GetShipmentSalesOrdersRequest : IRequest<GetShipmentSalesOrdersResponse> { }
public class GetShipmentSalesOrdersResponse
{
    public IEnumerable<ShipmentSalesOrderDto> ShipmentSalesOrders { get; set; }
}
public class GetShipmentSalesOrdersHandler : IRequestHandler<GetShipmentSalesOrdersRequest, GetShipmentSalesOrdersResponse>
{
    private readonly IAppDbContext _context;
    public GetShipmentSalesOrdersHandler(IAppDbContext context) => _context = context;
    public async Task<GetShipmentSalesOrdersResponse> Handle(GetShipmentSalesOrdersRequest request, CancellationToken cancellationToken)
        => new GetShipmentSalesOrdersResponse()
        {
            ShipmentSalesOrders = await _context.ShipmentSalesOrders.Select(x => x.ToDto()).ToArrayAsync()
        };
}
