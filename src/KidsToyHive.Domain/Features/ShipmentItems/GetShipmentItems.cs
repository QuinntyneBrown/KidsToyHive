using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems;

public class GetShipmentItemsRequest : IRequest<GetShipmentItemsResponse> { }
public class GetShipmentItemsResponse
{
    public IEnumerable<ShipmentItemDto> ShipmentItems { get; set; }
}
public class GetShipmentItemsHandler : IRequestHandler<GetShipmentItemsRequest, GetShipmentItemsResponse>
{
    private readonly IAppDbContext _context;
    public GetShipmentItemsHandler(IAppDbContext context) => _context = context;
    public async Task<GetShipmentItemsResponse> Handle(GetShipmentItemsRequest request, CancellationToken cancellationToken)
        => new GetShipmentItemsResponse()
        {
            ShipmentItems = await _context.ShipmentItems.Select(x => x.ToDto()).ToArrayAsync()
        };
}
