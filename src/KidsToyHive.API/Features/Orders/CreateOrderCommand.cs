using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KidsToyHive.API.Features.Orders
{
    public class CreateOrderCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Order.OrderId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public OrderDto Order { get; set; }
        }

        public class Response
        {			
            public Guid OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var order = new Order(request.Order.Name);

                _eventStore.Save(order);
                
                return Task.FromResult(new Response() { OrderId = order.OrderId });
            }
        }
    }
}
