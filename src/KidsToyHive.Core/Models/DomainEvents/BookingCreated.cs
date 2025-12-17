// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;

namespace KidsToyHive.Core.Models.DomainEvents;

public class BookingCreated : INotification
{
    public BookingCreated(Booking booking)
    {
        Booking = booking;
    }
    public Booking Booking { get; private set; }
}

