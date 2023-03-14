using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Dashboards;

public class UpsertDashboardValidator : AbstractValidator<UpsertDashboardRequest>
{
    public UpsertDashboardValidator()
    {
        RuleFor(request => request.Dashboard).NotNull();
        RuleFor(request => request.Dashboard).SetValidator(new DashboardDtoValidator());
    }
}
public class UpsertDashboardRequest : IRequest<UpsertDashboardResponse>
{
    public DashboardDto Dashboard { get; set; }
}
public class UpsertDashboardResponse
{
    public Guid DashboardId { get; set; }
}
public class UpsertDashboardHandler : IRequestHandler<UpsertDashboardRequest, UpsertDashboardResponse>
{
    private readonly IAppDbContext _context;
    public UpsertDashboardHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertDashboardResponse> Handle(UpsertDashboardRequest request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards.FindAsync(request.Dashboard.DashboardId);
        if (dashboard == null)
        {
            dashboard = new Dashboard();
            _context.Dashboards.Add(dashboard);
        }
        dashboard.Name = request.Dashboard.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertDashboardResponse() { DashboardId = dashboard.DashboardId };
    }
}
