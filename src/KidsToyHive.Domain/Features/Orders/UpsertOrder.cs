using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Orders
{
    public class UpsertOrder
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Order).NotNull();
                RuleFor(request => request.Order).SetValidator(new OrderDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public OrderDto Order { get; set; }
        }

        public class Response
        {
            public Guid OrderId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var order = await _context.Orders.FindAsync(request.Order.OrderId);

                if (order == null) {
                    order = new Order();
                    _context.Orders.Add(order);
                }

                order.Name = request.Order.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { OrderId = order.OrderId };
            }
        }
    }
}
