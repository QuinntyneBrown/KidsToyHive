using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments;

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
    private readonly IAppDbContext _context;
    public RemoveShipmentHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveShipmentRequest request, CancellationToken cancellationToken)
    {
        var shipment = await _context.Shipments.FindAsync(request.ShipmentId);
        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
