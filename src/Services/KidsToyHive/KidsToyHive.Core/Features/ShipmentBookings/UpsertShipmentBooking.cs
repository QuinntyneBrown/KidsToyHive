// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentBookings;

public class UpsertShipmentBookingValidator : AbstractValidator<UpsertShipmentBookingRequest>
{
    public UpsertShipmentBookingValidator()
    {
        RuleFor(request => request.ShipmentBooking).NotNull();
        RuleFor(request => request.ShipmentBooking).SetValidator(new ShipmentBookingDtoValidator());
    }
}
public class UpsertShipmentBookingRequest : IRequest<UpsertShipmentBookingResponse>
{
    public ShipmentBookingDto ShipmentBooking { get; set; }
}
public class UpsertShipmentBookingResponse
{
    public Guid ShipmentBookingId { get; set; }
    public int Version { get; set; }
}
public class UpsertShipmentBookingHandler : IRequestHandler<UpsertShipmentBookingRequest, UpsertShipmentBookingResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertShipmentBookingHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertShipmentBookingResponse> Handle(UpsertShipmentBookingRequest request, CancellationToken cancellationToken)
    {
        var shipmentBooking = await _context.ShipmentBookings.FindAsync(request.ShipmentBooking.ShipmentBookingId);
        if (shipmentBooking == null)
        {
            shipmentBooking = new ShipmentBooking();
            _context.ShipmentBookings.Add(shipmentBooking);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertShipmentBookingResponse()
        {
            ShipmentBookingId = shipmentBooking.ShipmentBookingId,
            Version = shipmentBooking.Version
        };
    }
}

