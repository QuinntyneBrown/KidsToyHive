using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KidsToyHive.API.Features.Orders
{
    public class CreateOrderItemCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.OrderItem.OrderItemId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public OrderItemDto OrderItem { get; set; }
        }

        public class Response
        {			
            public Guid OrderItemId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var orderItem = new OrderItem(request.OrderItem.Name);

                _eventStore.Save(orderItem);
                
                return Task.FromResult(new Response() { OrderItemId = orderItem.OrderItemId });
            }
        }
    }
}
