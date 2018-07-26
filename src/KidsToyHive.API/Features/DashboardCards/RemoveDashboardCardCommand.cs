using KidsToyHive.Core.DomainEvents;
using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KidsToyHive.API.Features.DashboardCards
{
    public class RemoveDashboardCardCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardCardId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid DashboardCardId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                
                var dashboardCard = _eventStore.Query<DashboardCard>(request.DashboardCardId);

                var dashboard = _eventStore.Query<Dashboard>(dashboardCard.DashboardId);

                dashboard.RemoveDashboardCard(dashboardCard.DashboardCardId);

                dashboardCard.Remove();

                _eventStore.Save(dashboardCard);

                _eventStore.Save(dashboard);

                return Task.CompletedTask;
            }
        }
    }
}
