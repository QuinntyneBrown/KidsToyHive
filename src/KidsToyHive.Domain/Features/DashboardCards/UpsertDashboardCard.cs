using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DashboardCards;

public class UpsertDashboardCard
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.DashboardCard).NotNull();
            RuleFor(request => request.DashboardCard).SetValidator(new DashboardCardDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public DashboardCardDto DashboardCard { get; set; }
    }
    public class Response
    {
        public Guid DashboardCardId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var dashboardCard = await _context.DashboardCards.FindAsync(request.DashboardCard.DashboardCardId);
            if (dashboardCard == null)
            {
                dashboardCard = new DashboardCard();
                _context.DashboardCards.Add(dashboardCard);
            }
            dashboardCard.Name = request.DashboardCard.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { DashboardCardId = dashboardCard.DashboardCardId };
        }
    }
}
