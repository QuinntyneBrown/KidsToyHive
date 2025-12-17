// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentSalesOrders;

public class RemoveShipmentSalesOrderValidator : AbstractValidator<RemoveShipmentSalesOrderRequest>
{
    public RemoveShipmentSalesOrderValidator()
    {
        RuleFor(request => request.ShipmentSalesOrderId).NotNull();
    }
}
public class RemoveShipmentSalesOrderRequest : IRequest
{
    public Guid ShipmentSalesOrderId { get; set; }
}
public class RemoveShipmentSalesOrderHandler : IRequestHandler<RemoveShipmentSalesOrderRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveShipmentSalesOrderHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveShipmentSalesOrderRequest request, CancellationToken cancellationToken)
    {
        var shipmentSalesOrder = await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrderId);
        _context.ShipmentSalesOrders.Remove(shipmentSalesOrder);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

