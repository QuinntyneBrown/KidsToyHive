// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentItems;

public class RemoveShipmentItemValidator : AbstractValidator<RemoveShipmentItemRequest>
{
    public RemoveShipmentItemValidator()
    {
        RuleFor(request => request.ShipmentItemId).NotNull();
    }
}
public class RemoveShipmentItemRequest : IRequest
{
    public Guid ShipmentItemId { get; set; }
}
public class RemoveShipmentItemHandler : IRequestHandler<RemoveShipmentItemRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveShipmentItemHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveShipmentItemRequest request, CancellationToken cancellationToken)
    {
        var shipmentItem = await _context.ShipmentItems.FindAsync(request.ShipmentItemId);
        _context.ShipmentItems.Remove(shipmentItem);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

