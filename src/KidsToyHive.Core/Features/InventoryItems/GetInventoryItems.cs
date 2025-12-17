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

namespace KidsToyHive.Core.Features.InventoryItems;

public class GetInventoryItemsRequest : IRequest<GetInventoryItemsResponse> { }
public class GetInventoryItemsResponse
{
    public IEnumerable<InventoryItemDto> InventoryItems { get; set; }
}
public class GetInventoryItemsHandler : IRequestHandler<GetInventoryItemsRequest, GetInventoryItemsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetInventoryItemsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetInventoryItemsResponse> Handle(GetInventoryItemsRequest request, CancellationToken cancellationToken)
        => new GetInventoryItemsResponse()
        {
            InventoryItems = await _context.InventoryItems.Select(x => x.ToDto()).ToArrayAsync()
        };
}

