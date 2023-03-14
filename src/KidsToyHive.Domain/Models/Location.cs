using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models;

public class Location : BaseModel
{
    public Guid LocationId { get; set; }
    public Address Adddress { get; set; }
    public string Description { get; set; }
    public LocationType Type { get; set; } = LocationType.Delivery;
    public ICollection<BookingDetail> BookingDetails { get; set; }
        = new HashSet<BookingDetail>();
    public ICollection<Booking> Bookings { get; set; }
        = new HashSet<Booking>();
}
