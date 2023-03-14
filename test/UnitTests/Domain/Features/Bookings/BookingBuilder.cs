using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Features.Addresses;
using KidsToyHive.Domain.Features.BookingDetails;
using KidsToyHive.Domain.Features.Bookings;
using KidsToyHive.Domain.Features.Locations;
using KidsToyHive.Domain.Features.Products;
using KidsToyHive.Domain.Models;
using System;
using System.Collections.Generic;

namespace UnitTests.Domain.Features.Bookings;

public class BookingBuilder
{
    public static BookingDto Build(BookingTimeSlot timeSlot, DateTime date, Product product)
    {
        var booking = new BookingDto
        {
            BookingTimeSlot = timeSlot,
            Date = date,
            BookingDetails = new List<BookingDetailDto> {
                     new BookingDetailDto {
                         Quantity = 1,
                         ProductId = product.ProductId,
                         Product = new ProductDto {
                             ProductId = product.ProductId,
                             ChargePeriodPrice = product.ChargePeriodPrice
                         }
                     }
                 },
            Location = new LocationDto
            {
                Address = new AddressDto
                {
                    Street = "628 Fleet Street",
                    City = "Toronto",
                    Province = "Ontario",
                    PostalCode = "M5V 1A8"
                }
            }
        };
        return booking;
    }
}
