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

namespace KidsToyHive.Core.Features.Shipments;

public class GetShipmentsRequest : IRequest<GetShipmentsResponse> { }
public class GetShipmentsResponse
{
    public IEnumerable<ShipmentDto> Shipments { get; set; }
}
public class GetShipmentsHandler : IRequestHandler<GetShipmentsRequest, GetShipmentsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetShipmentsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentsResponse> Handle(GetShipmentsRequest request, CancellationToken cancellationToken)
        => new GetShipmentsResponse()
        {
            Shipments = await _context.Shipments.Select(x => x.ToDto()).ToArrayAsync()
        };
}

