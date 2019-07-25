using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class ShipmentDelivered
    {
        public class Notification: INotification
        {
            public Guid ShipmentId { get; set; }
        }

        public class Handler : INotificationHandler<Notification>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context)
            {
                _context = context;
            }
            public async Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                var shipment = await _context.Shipments.Where(x => x.ShipmentId == notification.ShipmentId)
                    .Include(x => x.ShipmentBookings)
                    .ThenInclude(x => x.Booking)
                    .SingleAsync();

                var pickUpShipment = new Shipment {
                    ShipmentBookings = shipment.ShipmentBookings,
                    LocationId = shipment.LocationId,
                };

                await _context.Shipments.AddAsync(pickUpShipment);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
