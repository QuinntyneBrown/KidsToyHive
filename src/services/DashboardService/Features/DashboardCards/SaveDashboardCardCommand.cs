using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace DashboardService.Features.DashboardCards
{
    public class SaveDashboardCardCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.DashboardCard.DashboardCardId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public DashboardCardApiModel DashboardCard { get; set; }
        }

        public class Response
        {			
            public int DashboardCardId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboardCard = await _context.DashboardCards.FindAsync(request.DashboardCard.DashboardCardId);

                if (dashboardCard == null) _context.DashboardCards.Add(dashboardCard = new DashboardCard());
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { DashboardCardId = dashboardCard.DashboardCardId };
            }
        }
    }
}
