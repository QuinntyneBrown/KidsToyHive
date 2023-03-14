using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins;

public class GetBinsRequest : IRequest<GetBinsResponse> { }
public class GetBinsResponse
{
    public IEnumerable<BinDto> Bins { get; set; }
}
public class GetBinsHandler : IRequestHandler<GetBinsRequest, GetBinsResponse>
{
    private readonly IAppDbContext _context;
    public GetBinsHandler(IAppDbContext context) => _context = context;
    public async Task<GetBinsResponse> Handle(GetBinsRequest request, CancellationToken cancellationToken)
        => new GetBinsResponse()
        {
            Bins = await _context.Bins.Select(x => x.ToDto()).ToArrayAsync()
        };
}
