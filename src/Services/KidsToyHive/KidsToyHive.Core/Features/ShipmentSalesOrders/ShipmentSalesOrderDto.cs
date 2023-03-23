// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.ShipmentSalesOrders;

public class ShipmentSalesOrderDtoValidator : AbstractValidator<ShipmentSalesOrderDto>
{
    public ShipmentSalesOrderDtoValidator()
    {
        RuleFor(x => x.ShipmentSalesOrderId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class ShipmentSalesOrderDto
{
    public Guid ShipmentSalesOrderId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class ShipmentSalesOrderExtensions
{
    public static ShipmentSalesOrderDto ToDto(this ShipmentSalesOrder shipmentSalesOrder)
        => new ShipmentSalesOrderDto
        {
            ShipmentSalesOrderId = shipmentSalesOrder.ShipmentSalesOrderId,
            Name = shipmentSalesOrder.Name,
            Version = shipmentSalesOrder.Version
        };
}

