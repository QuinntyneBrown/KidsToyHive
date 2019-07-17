using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.OrderItems
{
    public class UpsertOrderItem
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.OrderItem).NotNull();
                RuleFor(request => request.OrderItem).SetValidator(new OrderItemDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public OrderItemDto OrderItem { get; set; }
        }

        public class Response
        {
            public Guid OrderItemId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var orderItem = await _context.OrderItems.FindAsync(request.OrderItem.OrderItemId);

                if (orderItem == null) {
                    orderItem = new OrderItem();
                    _context.OrderItems.Add(orderItem);
                }

                orderItem.Name = request.OrderItem.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { OrderItemId = orderItem.OrderItemId };
            }
        }
    }
}
