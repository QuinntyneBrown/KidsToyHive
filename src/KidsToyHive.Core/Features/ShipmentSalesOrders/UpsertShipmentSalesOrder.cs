// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentSalesOrders;

public class UpsertShipmentSalesOrderValidator : AbstractValidator<UpsertShipmentSalesOrderRequest>
{
    public UpsertShipmentSalesOrderValidator()
    {
        RuleFor(request => request.ShipmentSalesOrder).NotNull();
        RuleFor(request => request.ShipmentSalesOrder).SetValidator(new ShipmentSalesOrderDtoValidator());
    }
}
public class UpsertShipmentSalesOrderRequest : IRequest<UpsertShipmentSalesOrderResponse>
{
    public ShipmentSalesOrderDto ShipmentSalesOrder { get; set; }
}
public class UpsertShipmentSalesOrderResponse
{
    public Guid ShipmentSalesOrderId { get; set; }
}
public class UpsertShipmentSalesOrderHandler : IRequestHandler<UpsertShipmentSalesOrderRequest, UpsertShipmentSalesOrderResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertShipmentSalesOrderHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertShipmentSalesOrderResponse> Handle(UpsertShipmentSalesOrderRequest request, CancellationToken cancellationToken)
    {
        var shipmentSalesOrder = await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrder.ShipmentSalesOrderId);
        if (shipmentSalesOrder == null)
        {
            shipmentSalesOrder = new ShipmentSalesOrder();
            _context.ShipmentSalesOrders.Add(shipmentSalesOrder);
        }
        shipmentSalesOrder.Name = request.ShipmentSalesOrder.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertShipmentSalesOrderResponse() { ShipmentSalesOrderId = shipmentSalesOrder.ShipmentSalesOrderId };
    }
}

