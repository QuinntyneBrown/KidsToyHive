using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments;

public class UpsertShipmentValidator : AbstractValidator<UpsertShipmentRequest>
{
    public UpsertShipmentValidator()
    {
        RuleFor(request => request.Shipment).NotNull();
        RuleFor(request => request.Shipment).SetValidator(new ShipmentDtoValidator());
    }
}
public class UpsertShipmentRequest : IRequest<UpsertShipmentResponse>
{
    public ShipmentDto Shipment { get; set; }
}
public class UpsertShipmentResponse
{
    public Guid ShipmentId { get; set; }
}
public class UpsertShipmentHandler : IRequestHandler<UpsertShipmentRequest, UpsertShipmentResponse>
{
    private readonly IAppDbContext _context;
    public UpsertShipmentHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertShipmentResponse> Handle(UpsertShipmentRequest request, CancellationToken cancellationToken)
    {
        var shipment = await _context.Shipments.FindAsync(request.Shipment.ShipmentId);
        if (shipment == null)
        {
            shipment = new Shipment();
            _context.Shipments.Add(shipment);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertShipmentResponse() { ShipmentId = shipment.ShipmentId };
    }
}
