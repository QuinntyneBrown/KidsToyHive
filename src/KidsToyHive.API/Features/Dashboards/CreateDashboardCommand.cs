using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KidsToyHive.API.Features.Dashboards
{
    public class CreateDashboardCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Dashboard.DashboardId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public DashboardDto Dashboard { get; set; }
        }

        public class Response
        {        	
            public Guid DashboardId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
        	public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = new Dashboard(request.Dashboard.Name, request.Dashboard.UserId);

                _eventStore.Save(dashboard);
                
                return Task.FromResult(new Response() { DashboardId = dashboard.DashboardId });
            }
        }
    }
}
