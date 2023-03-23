// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentItems;

public class UpsertShipmentItemValidator : AbstractValidator<UpsertShipmentItemRequest>
{
    public UpsertShipmentItemValidator()
    {
        RuleFor(request => request.ShipmentItem).NotNull();
        RuleFor(request => request.ShipmentItem).SetValidator(new ShipmentItemDtoValidator());
    }
}
public class UpsertShipmentItemRequest : IRequest<UpsertShipmentItemResponse>
{
    public ShipmentItemDto ShipmentItem { get; set; }
}
public class UpsertShipmentItemResponse
{
    public Guid ShipmentItemId { get; set; }
}
public class UpsertShipmentItemHandler : IRequestHandler<UpsertShipmentItemRequest, UpsertShipmentItemResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public UpsertShipmentItemHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertShipmentItemResponse> Handle(UpsertShipmentItemRequest request, CancellationToken cancellationToken)
    {
        var shipmentItem = await _context.ShipmentItems.FindAsync(request.ShipmentItem.ShipmentItemId);
        if (shipmentItem == null)
        {
            shipmentItem = new ShipmentItem();
            _context.ShipmentItems.Add(shipmentItem);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertShipmentItemResponse() { ShipmentItemId = shipmentItem.ShipmentItemId };
    }
}

