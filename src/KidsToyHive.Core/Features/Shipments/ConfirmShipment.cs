// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core;
using KidsToyHive.Core.Models.DomainEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Shipments;

public class ConfirmShipmentValidator : AbstractValidator<ConfirmShipmentRequest>
{
    public ConfirmShipmentValidator()
    {
        RuleFor(x => x.ShipmentId).NotNull();
        RuleFor(x => x.SignatureId).NotNull();
    }
}
public class ConfirmShipmentRequest : IRequest<ConfirmShipmentResponse>
{
    public Guid ShipmentId { get; set; }
    public Guid SignatureId { get; set; }
}
public class ConfirmShipmentResponse
{
    public Guid ShipmentId { get; set; }
    public int Version { get; set; }
}
public class ConfirmShipmentHandler : IRequestHandler<ConfirmShipmentRequest, ConfirmShipmentResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    private readonly IMediator _mediator;
    public ConfirmShipmentHandler(IKidsToyHiveDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    public async Task<ConfirmShipmentResponse> Handle(ConfirmShipmentRequest request, CancellationToken cancellationToken)
    {
        var shipment = _context.Shipments.Find(request.ShipmentId);
        shipment.SignatureId = request.SignatureId;
        shipment.RaiseDomainEvent(new ShipmentCompleted
        {
            ShipmentId = shipment.ShipmentId
        });
        await _context.SaveChangesAsync(cancellationToken);
        return new ConfirmShipmentResponse()
        {
            ShipmentId = shipment.ShipmentId,
            Version = shipment.Version
        };
    }
}

