// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Sagas;

public class ShipmentCompletedSagaHandler : INotificationHandler<ShipmentCompleted>
{
    private readonly IKidsToyHiveDbContext _context;
    public ShipmentCompletedSagaHandler(IKidsToyHiveDbContext context)
    {
        _context = context;
    }
    public Task Handle(ShipmentCompleted notification, CancellationToken cancellationToken)
    {
        //TO DO: create pickup shipment if shipment contained bookings

        throw new NotImplementedException();
    }
}

