using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Sagas;

public class ShipmentCompletedSagaHandler : INotificationHandler<ShipmentCompleted>
{
    private readonly IAppDbContext _context;
    public ShipmentCompletedSagaHandler(IAppDbContext context)
    {
        _context = context;
    }
    public Task Handle(ShipmentCompleted notification, CancellationToken cancellationToken)
    {
        //TO DO: create pickup shipment if shipment contained bookings

        throw new NotImplementedException();
    }
}
