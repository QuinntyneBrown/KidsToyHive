using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DashboardCards
{
    public class GetDashboardCardById
    {
        public class Request : IRequest<Response> {
            public Guid DashboardCardId { get; set; }
        }

        public class Response
        {
            public DashboardCardDto DashboardCard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DashboardCard = (await _context.DashboardCards.FindAsync(request.DashboardCardId)).ToDto()
                };
        }
    }
}
