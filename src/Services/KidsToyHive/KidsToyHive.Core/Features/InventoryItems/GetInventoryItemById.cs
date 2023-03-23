// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.InventoryItems;

public class GetInventoryItemByIdRequest : IRequest<GetInventoryItemByIdResponse>
{
    public Guid InventoryItemId { get; set; }
}
public class GetInventoryItemByIdResponse
{
    public InventoryItemDto InventoryItem { get; set; }
}
public class GetInventoryItemByIdHandler : IRequestHandler<GetInventoryItemByIdRequest, GetInventoryItemByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetInventoryItemByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetInventoryItemByIdResponse> Handle(GetInventoryItemByIdRequest request, CancellationToken cancellationToken)
        => new GetInventoryItemByIdResponse()
        {
            InventoryItem = (await _context.InventoryItems.FindAsync(request.InventoryItemId)).ToDto()
        };
}

