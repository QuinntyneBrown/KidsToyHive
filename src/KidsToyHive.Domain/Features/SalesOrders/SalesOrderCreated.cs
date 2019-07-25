using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders
{
    public class SalesOrderCreated
    {
        public class Notification: INotification
        {

        }

        public class Handler : INotificationHandler<Notification>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
