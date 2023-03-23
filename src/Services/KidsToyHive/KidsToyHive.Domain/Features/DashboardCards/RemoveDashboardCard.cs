using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DashboardCards;

public class RemoveDashboardCardValidator : AbstractValidator<RemoveDashboardCardRequest>
{
    public RemoveDashboardCardValidator()
    {
        RuleFor(request => request.DashboardCardId).NotNull();
    }
}
public class RemoveDashboardCardRequest : IRequest
{
    public Guid DashboardCardId { get; set; }
}
public class RemoveDashboardCardHandler : IRequestHandler<RemoveDashboardCardRequest>
{
    private readonly IAppDbContext _context;
    public RemoveDashboardCardHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveDashboardCardRequest request, CancellationToken cancellationToken)
    {
        var dashboardCard = await _context.DashboardCards.FindAsync(request.DashboardCardId);
        _context.DashboardCards.Remove(dashboardCard);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
