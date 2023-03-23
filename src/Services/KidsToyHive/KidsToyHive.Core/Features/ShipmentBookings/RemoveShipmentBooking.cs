// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentBookings;

public class RemoveShipmentBookingValidator : AbstractValidator<RemoveShipmentBookingRequest>
{
    public RemoveShipmentBookingValidator()
    {
        RuleFor(request => request.ShipmentBookingId).NotNull();
    }
}
public class RemoveShipmentBookingRequest : IRequest
{
    public Guid ShipmentBookingId { get; set; }
}
public class RemoveShipmentBookingHandler : IRequestHandler<RemoveShipmentBookingRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveShipmentBookingHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveShipmentBookingRequest request, CancellationToken cancellationToken)
    {
        var shipmentBooking = await _context.ShipmentBookings.FindAsync(request.ShipmentBookingId);
        _context.ShipmentBookings.Remove(shipmentBooking);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

