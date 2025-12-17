// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.SalesOrders;

public class Notification : INotification
{
}
public class SalesOrderCreatedHandler : INotificationHandler<Notification>
{
    private readonly IKidsToyHiveDbContext _context;
    public SalesOrderCreatedHandler(IKidsToyHiveDbContext context)
    {
        _context = context;
    }
    public Task Handle(Notification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

