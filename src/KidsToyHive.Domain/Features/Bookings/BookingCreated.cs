using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class BookingCreated
    {
        public class Notification : INotification
        {
            public Guid BookingId { get; set; }
        }

        public class Handler : INotificationHandler<Notification>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
                => _context = context;

            public async Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings.FindAsync(notification.BookingId);

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
