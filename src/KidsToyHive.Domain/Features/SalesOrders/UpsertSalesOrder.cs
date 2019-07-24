using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders
{
    public class UpsertSalesOrder
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.SalesOrder).NotNull();
                RuleFor(request => request.SalesOrder).SetValidator(new SalesOrderDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public SalesOrderDto SalesOrder { get; set; }
        }

        public class Response
        {
            public Guid SalesOrderId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var salesOrder = await _context.SalesOrders.FindAsync(request.SalesOrder.SalesOrderId);

                if (salesOrder == null) {
                    salesOrder = new SalesOrder();
                    _context.SalesOrders.Add(salesOrder);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { SalesOrderId = salesOrder.SalesOrderId };
            }
        }
    }
}
