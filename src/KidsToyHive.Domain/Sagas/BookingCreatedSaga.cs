using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Models.DomainEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class BookingCreatedSaga
    {
        public class Handler : INotificationHandler<BookingCreated>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
                => _context = context;

            public async Task Handle(BookingCreated notification, CancellationToken cancellationToken)
            {
                var booking = notification.Booking;

                var shippingBooking = new ShipmentBooking()
                {
                    BookingId = booking.BookingId
                };

                var shipment = new Shipment()
                {
                    Type = ShipmentType.Delivery,
                    LocationId = booking.LocationId
                };

                shipment.ShipmentBookings.Add(shippingBooking);

                await _context.Shipments.AddAsync(shipment);
            }
        }
    }
}
