using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DashboardCards;

public class GetDashboardCardsRequest : IRequest<GetDashboardCardsResponse> { }
public class GetDashboardCardsResponse
{
    public IEnumerable<DashboardCardDto> DashboardCards { get; set; }
}
public class GetDashboardCardsHandler : IRequestHandler<GetDashboardCardsRequest, GetDashboardCardsResponse>
{
    private readonly IAppDbContext _context;
    public GetDashboardCardsHandler(IAppDbContext context) => _context = context;
    public async Task<GetDashboardCardsResponse> Handle(GetDashboardCardsRequest request, CancellationToken cancellationToken)
        => new GetDashboardCardsResponse()
        {
            DashboardCards = await _context.DashboardCards.Select(x => x.ToDto()).ToArrayAsync()
        };
}
