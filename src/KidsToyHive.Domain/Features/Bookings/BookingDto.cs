using KidsToyHive.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using KidsToyHive.Domain.Features.BookingDetails;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Features.Customers;
using KidsToyHive.Domain.Features.Locations;
using System.Linq;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class BookingDtoValidator: AbstractValidator<BookingDto>
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
}
