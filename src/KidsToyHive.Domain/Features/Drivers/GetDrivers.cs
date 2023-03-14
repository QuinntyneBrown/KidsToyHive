using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers;

public class GetDriversRequest : IRequest<GetDriversResponse> { }
public class GetDriversResponse
{
    public IEnumerable<DriverDto> Drivers { get; set; }
}
public class GetDriversHandler : IRequestHandler<GetDriversRequest, GetDriversResponse>
{
    private readonly IAppDbContext _context;
    public GetDriversHandler(IAppDbContext context) => _context = context;
    public async Task<GetDriversResponse> Handle(GetDriversRequest request, CancellationToken cancellationToken)
        => new GetDriversResponse()
        {
            Drivers = await _context.Drivers.Select(x => x.ToDto()).ToArrayAsync()
        };
}
