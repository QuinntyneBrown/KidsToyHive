using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Customers
{
    public class GetCustomerByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CustomerId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid CustomerId { get; set; }
        }

        public class Response
        {
            public CustomerDto Customer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Customer = CustomerDto.FromCustomer(_eventStore.Query<Customer>(request.CustomerId))
                });
        }
    }
}
