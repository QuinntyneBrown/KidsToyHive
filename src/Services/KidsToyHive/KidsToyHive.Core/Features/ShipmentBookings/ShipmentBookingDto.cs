// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.ShipmentBookings;

public class ShipmentBookingDtoValidator : AbstractValidator<ShipmentBookingDto>
{
    public ShipmentBookingDtoValidator()
    {
        RuleFor(x => x.ShipmentBookingId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class ShipmentBookingDto
{
    public Guid ShipmentBookingId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class ShipmentBookingExtensions
{
    public static ShipmentBookingDto ToDto(this ShipmentBooking shipmentBooking)
        => new ShipmentBookingDto
        {
            ShipmentBookingId = shipmentBooking.ShipmentBookingId,
            Name = shipmentBooking.Name,
            Version = shipmentBooking.Version
        };
}

