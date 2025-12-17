// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentSalesOrders;

public class GetShipmentSalesOrderByIdRequest : IRequest<GetShipmentSalesOrderByIdResponse>
{
    public Guid ShipmentSalesOrderId { get; set; }
}
public class GetShipmentSalesOrderByIdResponse
{
    public ShipmentSalesOrderDto ShipmentSalesOrder { get; set; }
}
public class GetShipmentSalesOrderByIdHandler : IRequestHandler<GetShipmentSalesOrderByIdRequest, GetShipmentSalesOrderByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetShipmentSalesOrderByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentSalesOrderByIdResponse> Handle(GetShipmentSalesOrderByIdRequest request, CancellationToken cancellationToken)
        => new GetShipmentSalesOrderByIdResponse()
        {
            ShipmentSalesOrder = (await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrderId)).ToDto()
        };
}

