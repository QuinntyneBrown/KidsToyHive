// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using KidsToyHive.Core.Features.BookingDetails;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Features.Customers;
using KidsToyHive.Core.Features.Locations;
using System.Linq;

namespace KidsToyHive.Core.Features.Bookings;

public class BookingDtoValidator : AbstractValidator<BookingDto>
{
    public BookingDtoValidator()
    {
        RuleFor(x => x.BookingId).NotNull();
        RuleFor(x => x.CustomerId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class BookingDto
{
    public Guid BookingId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? LocationId { get; set; }
    public LocationDto Location { get; set; }
    public CustomerDto Customer { get; set; }
    public ICollection<BookingDetailDto> BookingDetails { get; set; }
        = new HashSet<BookingDetailDto>();
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public BookingTimeSlot BookingTimeSlot { get; set; }
    public int Version { get; set; }
}
public static class BookingExtensions
{
    public static BookingDto ToDto(this Booking booking)
        => new BookingDto
        {
            BookingId = booking.BookingId,
            CustomerId = booking.CustomerId,
            Name = booking.Name,
            BookingTimeSlot = booking.BookingTimeSlot,
            LocationId = booking.LocationId,
            Version = booking.Version,
            BookingDetails = booking.BookingDetails.Select(x => x.ToDto()).ToList(),
            Date = booking.Date
        };
}

