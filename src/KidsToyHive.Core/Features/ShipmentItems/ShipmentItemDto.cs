// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.ShipmentItems;

public class ShipmentItemDtoValidator : AbstractValidator<ShipmentItemDto>
{
    public ShipmentItemDtoValidator()
    {
        RuleFor(x => x.ShipmentItemId).NotNull();
    }
}
public class ShipmentItemDto
{
    public Guid ShipmentItemId { get; set; }
    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
    public Guid? SalesOrderDetailId { get; set; }
    public Guid? BookingDetailId { get; set; }
    public int Quantity { get; set; }
    public string Comments { get; set; }
    public int Version { get; set; }
}
public static class ShipmentItemExtensions
{
    public static ShipmentItemDto ToDto(this ShipmentItem shipmentItem)
        => new ShipmentItemDto
        {
            ShipmentItemId = shipmentItem.ShipmentItemId,
            ShipmentId = shipmentItem.ShipmentId,
            SalesOrderDetailId = shipmentItem.SalesOrderDetailId,
            BookingDetailId = shipmentItem.BookingDetailId,
            Quantity = shipmentItem.Quantity,
            Version = shipmentItem.Version
        };
}

