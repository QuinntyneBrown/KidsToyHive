using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KidsToyHive.API.Features.Customers
{
    public class RemoveCustomerCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CustomerId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var customer = _eventStore.Query<Customer>(request.CustomerId);

                customer.Remove();
                
                _eventStore.Save(customer);

                return Task.CompletedTask;
            }
        }
    }
}
