using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain;
using KidsToyHive.Domain.Models.DomainEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments;

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
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    public ConfirmShipmentHandler(IAppDbContext context, IMediator mediator)
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
