// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Warehouses;

public class GetWarehouseByIdRequest : IRequest<GetWarehouseByIdResponse>
{
    public Guid WarehouseId { get; set; }
}
public class GetWarehouseByIdResponse
{
    public WarehouseDto Warehouse { get; set; }
}
public class GetWarehouseByIdHandler : IRequestHandler<GetWarehouseByIdRequest, GetWarehouseByIdResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetWarehouseByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetWarehouseByIdResponse> Handle(GetWarehouseByIdRequest request, CancellationToken cancellationToken)
        => new GetWarehouseByIdResponse()
        {
            Warehouse = (await _context.Warehouses.FindAsync(request.WarehouseId)).ToDto()
        };
}

