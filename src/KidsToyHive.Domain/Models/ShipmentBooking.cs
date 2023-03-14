using System;

namespace KidsToyHive.Domain.Models;

public class ShipmentBooking
{
    public Guid ShipmentBookingId { get; set; }
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
