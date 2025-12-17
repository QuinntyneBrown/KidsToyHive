// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Shipments;

public class RemoveShipmentValidator : AbstractValidator<RemoveShipmentRequest>
{
    public RemoveShipmentValidator()
    {
        RuleFor(request => request.ShipmentId).NotNull();
    }
}
public class RemoveShipmentRequest : IRequest
{
    public Guid ShipmentId { get; set; }
}
public class RemoveShipmentHandler : IRequestHandler<RemoveShipmentRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveShipmentHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveShipmentRequest request, CancellationToken cancellationToken)
    {
        var shipment = await _context.Shipments.FindAsync(request.ShipmentId);
        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

