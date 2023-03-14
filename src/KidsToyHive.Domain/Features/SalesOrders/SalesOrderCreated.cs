using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders;

 public class Notification : INotification
 {
 }
 public class SalesOrderCreatedHandler : INotificationHandler<Notification>
 {
     private readonly IAppDbContext _context;
     public SalesOrderCreatedHandler(IAppDbContext context)
     {
         _context = context;
     }
     public Task Handle(Notification notification, CancellationToken cancellationToken)
     {
         throw new NotImplementedException();
     }
 }
