using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Dashboards;

public class GetDashboardsRequest : IRequest<GetDashboardsResponse> { }
public class GetDashboardsResponse
{
    public IEnumerable<DashboardDto> Dashboards { get; set; }
}
public class GetDashboardsHandler : IRequestHandler<GetDashboardsRequest, GetDashboardsResponse>
{
    private readonly IAppDbContext _context;
    public GetDashboardsHandler(IAppDbContext context) => _context = context;
    public async Task<GetDashboardsResponse> Handle(GetDashboardsRequest request, CancellationToken cancellationToken)
        => new GetDashboardsResponse()
        {
            Dashboards = await _context.Dashboards.Select(x => x.ToDto()).ToArrayAsync()
        };
}
