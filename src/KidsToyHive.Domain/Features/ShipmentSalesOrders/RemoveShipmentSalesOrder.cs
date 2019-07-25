using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentSalesOrders
{
    public class RemoveShipmentSalesOrder
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShipmentSalesOrderId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid ShipmentSalesOrderId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var shipmentSalesOrder = await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrderId);

                _context.ShipmentSalesOrders.Remove(shipmentSalesOrder);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
