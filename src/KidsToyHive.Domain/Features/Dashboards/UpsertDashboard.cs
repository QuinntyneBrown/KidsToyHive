using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Dashboards;

public class UpsertDashboard
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.Dashboard).NotNull();
            RuleFor(request => request.Dashboard).SetValidator(new DashboardDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public DashboardDto Dashboard { get; set; }
    }
    public class Response
    {
        public Guid DashboardId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var dashboard = await _context.Dashboards.FindAsync(request.Dashboard.DashboardId);
            if (dashboard == null)
            {
                dashboard = new Dashboard();
                _context.Dashboards.Add(dashboard);
            }
            dashboard.Name = request.Dashboard.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { DashboardId = dashboard.DashboardId };
        }
    }
}
