using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KidsToyHive.API.Features.Orders
{
    public class RemoveOrderItemCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.OrderItemId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid OrderItemId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var orderItem = _eventStore.Query<OrderItem>(request.OrderItemId);

                orderItem.Remove();
                
                _eventStore.Save(orderItem);

                return Task.CompletedTask;
            }
        }
    }
}
