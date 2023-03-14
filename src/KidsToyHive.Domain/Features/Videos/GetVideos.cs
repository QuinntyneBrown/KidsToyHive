using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Videos;

public class GetVideosRequest : IRequest<GetVideosResponse> { }
public class GetVideosResponse
{
    public IEnumerable<VideoDto> Videos { get; set; }
}
public class GetVideosHandler : IRequestHandler<GetVideosRequest, GetVideosResponse>
{
    private readonly IAppDbContext _context;
    public GetVideosHandler(IAppDbContext context) => _context = context;
    public async Task<GetVideosResponse> Handle(GetVideosRequest request, CancellationToken cancellationToken)
        => new GetVideosResponse()
        {
            Videos = await _context.Videos.Select(x => x.ToDto()).ToArrayAsync()
        };
}
