using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DashboardCards;

public class RemoveDashboardCard
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.DashboardCardId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid DashboardCardId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var dashboardCard = await _context.DashboardCards.FindAsync(request.DashboardCardId);
            _context.DashboardCards.Remove(dashboardCard);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
