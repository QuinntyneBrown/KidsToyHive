using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Warehouses;

public class GetWarehousesRequest : IRequest<GetWarehousesResponse> { }
public class GetWarehousesResponse
{
    public IEnumerable<WarehouseDto> Warehouses { get; set; }
}
public class GetWarehousesHandler : IRequestHandler<GetWarehousesRequest, GetWarehousesResponse>
{
    private readonly IAppDbContext _context;
    public GetWarehousesHandler(IAppDbContext context) => _context = context;
    public async Task<GetWarehousesResponse> Handle(GetWarehousesRequest request, CancellationToken cancellationToken)
        => new GetWarehousesResponse()
        {
            Warehouses = await _context.Warehouses.Select(x => x.ToDto()).ToArrayAsync()
        };
}
