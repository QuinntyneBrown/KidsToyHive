using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.InventoryItems;

public class GetInventoryItemsRequest : IRequest<GetInventoryItemsResponse> { }
public class GetInventoryItemsResponse
{
    public IEnumerable<InventoryItemDto> InventoryItems { get; set; }
}
public class GetInventoryItemsHandler : IRequestHandler<GetInventoryItemsRequest, GetInventoryItemsResponse>
{
    private readonly IAppDbContext _context;
    public GetInventoryItemsHandler(IAppDbContext context) => _context = context;
    public async Task<GetInventoryItemsResponse> Handle(GetInventoryItemsRequest request, CancellationToken cancellationToken)
        => new GetInventoryItemsResponse()
        {
            InventoryItems = await _context.InventoryItems.Select(x => x.ToDto()).ToArrayAsync()
        };
}
