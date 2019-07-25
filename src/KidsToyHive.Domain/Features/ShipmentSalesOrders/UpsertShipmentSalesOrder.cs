using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentSalesOrders
{
    public class UpsertShipmentSalesOrder
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ShipmentSalesOrder).NotNull();
                RuleFor(request => request.ShipmentSalesOrder).SetValidator(new ShipmentSalesOrderDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public ShipmentSalesOrderDto ShipmentSalesOrder { get; set; }
        }

        public class Response
        {
            public Guid ShipmentSalesOrderId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var shipmentSalesOrder = await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrder.ShipmentSalesOrderId);

                if (shipmentSalesOrder == null) {
                    shipmentSalesOrder = new ShipmentSalesOrder();
                    _context.ShipmentSalesOrders.Add(shipmentSalesOrder);
                }

                shipmentSalesOrder.Name = request.ShipmentSalesOrder.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ShipmentSalesOrderId = shipmentSalesOrder.ShipmentSalesOrderId };
            }
        }
    }
}
