// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class ShipmentBooking
{
    public Guid ShipmentBookingId { get; set; }
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}

