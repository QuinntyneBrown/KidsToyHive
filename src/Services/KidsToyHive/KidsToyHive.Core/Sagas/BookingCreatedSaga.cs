// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using KidsToyHive.Core.Models.DomainEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Bookings;

public class BookingCreatedSagaHandler : INotificationHandler<BookingCreated>
{
    private readonly IKidsToyHiveDbContext _context;
    public BookingCreatedSagaHandler(IKidsToyHiveDbContext context)
        => _context = context;
    public async Task Handle(BookingCreated notification, CancellationToken cancellationToken)
    {
        var booking = notification.Booking;
        var customer = await _context
            .Customers
            .Include(x => x.CustomerLocations)
            .ThenInclude(x => x.Location)
            .SingleAsync(x => x.CustomerId == booking.CustomerId);
        var shippingBooking = new ShipmentBooking()
        {
            BookingId = booking.BookingId
        };
        var shipment = new Shipment()
        {
            Type = ShipmentType.Delivery,
        };
        var customerLocation = customer.CustomerLocations
            .SingleOrDefault(x => x.Location.Type == LocationType.Delivery
            || x.Location.Type == LocationType.DeliveryPickUp);
        if (customerLocation != null)
        {
            shipment.LocationId = customerLocation.Location.LocationId;
        }
        shipment.ShipmentBookings.Add(shippingBooking);
        await _context.Shipments.AddAsync(shipment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

