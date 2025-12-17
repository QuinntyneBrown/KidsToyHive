// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Warehouses;

public class GetWarehousesRequest : IRequest<GetWarehousesResponse> { }
public class GetWarehousesResponse
{
    public IEnumerable<WarehouseDto> Warehouses { get; set; }
}
public class GetWarehousesHandler : IRequestHandler<GetWarehousesRequest, GetWarehousesResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetWarehousesHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetWarehousesResponse> Handle(GetWarehousesRequest request, CancellationToken cancellationToken)
        => new GetWarehousesResponse()
        {
            Warehouses = await _context.Warehouses.Select(x => x.ToDto()).ToArrayAsync()
        };
}

