using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Orders
{
    public class GetOrderItemByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.OrderItemId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid OrderItemId { get; set; }
        }

        public class Response
        {
            public OrderItemDto OrderItem { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    OrderItem = OrderItemDto.FromOrderItem(_eventStore.Query<OrderItem>(request.OrderItemId))
                });
        }
    }
}
