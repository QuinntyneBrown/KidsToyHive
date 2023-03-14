using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Dashboards;

public class RemoveDashboardValidator : AbstractValidator<RemoveDashboardRequest>
{
    public RemoveDashboardValidator()
    {
        RuleFor(request => request.DashboardId).NotNull();
    }
}
public class RemoveDashboardRequest : IRequest
{
    public Guid DashboardId { get; set; }
}
public class RemoveDashboardHandler : IRequestHandler<RemoveDashboardRequest>
{
    private readonly IAppDbContext _context;
    public RemoveDashboardHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveDashboardRequest request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards.FindAsync(request.DashboardId);
        _context.Dashboards.Remove(dashboard);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
