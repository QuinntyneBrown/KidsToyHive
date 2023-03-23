using FluentValidation;
using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers;

public class CommitToShipmentValidator : AbstractValidator<CommitToShipmentRequest>
{
    public CommitToShipmentValidator()
    {
    }
}
public class CommitToShipmentRequest : IRequest<CommitToShipmentResponse>
{
    public Guid DriverId { get; set; }
    public Guid ShipmentId { get; set; }
}
public class CommitToShipmentResponse
{
}
public class CommitToShipmentHandler : IRequestHandler<CommitToShipmentRequest, CommitToShipmentResponse>
{
    private readonly IAppDbContext _context;
    public CommitToShipmentHandler(IAppDbContext context) => _context = context;
    public async Task<CommitToShipmentResponse> Handle(CommitToShipmentRequest request, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync(request.DriverId);
        var shipment = await _context.Shipments.FindAsync(request.ShipmentId);
        driver.Shipments.Add(shipment);
        await _context.SaveChangesAsync(cancellationToken);
        return new CommitToShipmentResponse() { };
    }
}
