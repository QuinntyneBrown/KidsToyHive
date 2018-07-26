using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.DashboardCards
{
    public class GetDashboardCardByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardCardId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid DashboardCardId { get; set; }
        }

        public class Response
        {
            public DashboardCardDto DashboardCard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
        	public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        	     => Task.FromResult(new Response()
                {
                    DashboardCard = DashboardCardDto.FromDashboardCard(_eventStore.Query<DashboardCard>(request.DashboardCardId))
                });
        }
    }
}
