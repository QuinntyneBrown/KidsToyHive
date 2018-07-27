using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KidsToyHive.API.Features.Orders
{
    public class RemoveOrderCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.OrderId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var order = _eventStore.Query<Order>(request.OrderId);

                order.Remove();
                
                _eventStore.Save(order);

                return Task.CompletedTask;
            }
        }
    }
}
