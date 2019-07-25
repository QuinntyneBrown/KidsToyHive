using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Dashboards
{
    public class GetDashboardById
    {
        public class Request : IRequest<Response> {
            public Guid DashboardId { get; set; }
        }

        public class Response
        {
            public DashboardDto Dashboard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Dashboard = (await _context.Dashboards.FindAsync(request.DashboardId)).ToDto()
                };
        }
    }
}
