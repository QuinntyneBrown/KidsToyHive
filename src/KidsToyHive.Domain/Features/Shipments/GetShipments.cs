using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments;

public class GetShipmentsRequest : IRequest<GetShipmentsResponse> { }
public class GetShipmentsResponse
{
    public IEnumerable<ShipmentDto> Shipments { get; set; }
}
public class GetShipmentsHandler : IRequestHandler<GetShipmentsRequest, GetShipmentsResponse>
{
    private readonly IAppDbContext _context;
    public GetShipmentsHandler(IAppDbContext context) => _context = context;
    public async Task<GetShipmentsResponse> Handle(GetShipmentsRequest request, CancellationToken cancellationToken)
        => new GetShipmentsResponse()
        {
            Shipments = await _context.Shipments.Select(x => x.ToDto()).ToArrayAsync()
        };
}
