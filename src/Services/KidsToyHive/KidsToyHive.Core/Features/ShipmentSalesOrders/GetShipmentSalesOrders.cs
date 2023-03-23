// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentSalesOrders;

public class GetShipmentSalesOrdersRequest : IRequest<GetShipmentSalesOrdersResponse> { }
public class GetShipmentSalesOrdersResponse
{
    public IEnumerable<ShipmentSalesOrderDto> ShipmentSalesOrders { get; set; }
}
public class GetShipmentSalesOrdersHandler : IRequestHandler<GetShipmentSalesOrdersRequest, GetShipmentSalesOrdersResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetShipmentSalesOrdersHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentSalesOrdersResponse> Handle(GetShipmentSalesOrdersRequest request, CancellationToken cancellationToken)
        => new GetShipmentSalesOrdersResponse()
        {
            ShipmentSalesOrders = await _context.ShipmentSalesOrders.Select(x => x.ToDto()).ToArrayAsync()
        };
}

