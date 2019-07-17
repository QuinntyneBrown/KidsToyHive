using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Dashboards
{
    public class RemoveDashboard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid DashboardId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _context.Dashboards.FindAsync(request.DashboardId);

                _context.Dashboards.Remove(dashboard);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
