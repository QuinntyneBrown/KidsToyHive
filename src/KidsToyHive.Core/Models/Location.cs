// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

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

