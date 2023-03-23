using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.DashboardCards;

public class GetDashboardCardByIdRequest : IRequest<GetDashboardCardByIdResponse>
{
    public Guid DashboardCardId { get; set; }
}
public class GetDashboardCardByIdResponse
{
    public DashboardCardDto DashboardCard { get; set; }
}
public class GetDashboardCardByIdHandler : IRequestHandler<GetDashboardCardByIdRequest, GetDashboardCardByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetDashboardCardByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetDashboardCardByIdResponse> Handle(GetDashboardCardByIdRequest request, CancellationToken cancellationToken)
        => new GetDashboardCardByIdResponse()
        {
            DashboardCard = (await _context.DashboardCards.FindAsync(request.DashboardCardId)).ToDto()
        };
}
