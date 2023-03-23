using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.ShipmentBookings;

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
