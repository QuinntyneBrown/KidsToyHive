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

namespace KidsToyHive.Core.Features.ShipmentItems;

public class GetShipmentItemsRequest : IRequest<GetShipmentItemsResponse> { }
public class GetShipmentItemsResponse
{
    public IEnumerable<ShipmentItemDto> ShipmentItems { get; set; }
}
public class GetShipmentItemsHandler : IRequestHandler<GetShipmentItemsRequest, GetShipmentItemsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetShipmentItemsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentItemsResponse> Handle(GetShipmentItemsRequest request, CancellationToken cancellationToken)
        => new GetShipmentItemsResponse()
        {
            ShipmentItems = await _context.ShipmentItems.Select(x => x.ToDto()).ToArrayAsync()
        };
}

