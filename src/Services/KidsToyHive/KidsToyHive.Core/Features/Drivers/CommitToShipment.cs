// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Drivers;

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
    private readonly IKidsToyHiveDbContext _context;
    public CommitToShipmentHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<CommitToShipmentResponse> Handle(CommitToShipmentRequest request, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync(request.DriverId);
        var shipment = await _context.Shipments.FindAsync(request.ShipmentId);
        driver.Shipments.Add(shipment);
        await _context.SaveChangesAsync(cancellationToken);
        return new CommitToShipmentResponse() { };
    }
}

