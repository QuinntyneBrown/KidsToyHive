// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Common;
using KidsToyHive.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Shipments;

public class GetCommittedShipmentsRequest : AuthenticatedRequest<GetCommittedShipmentsResponse>
{

}
public class GetCommittedShipmentsResponse
{
    public ICollection<ShipmentDto> Shipments { get; set; }
    = new HashSet<ShipmentDto>();
}
public class GetCommittedShipmentsHandler : IRequestHandler<GetCommittedShipmentsRequest, GetCommittedShipmentsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetCommittedShipmentsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetCommittedShipmentsResponse> Handle(GetCommittedShipmentsRequest request, CancellationToken cancellationToken)
    {

        var driver = await _context.Drivers.SingleAsync(x => x.Email == request.CurrentUsername);
        return new GetCommittedShipmentsResponse()
        {
            Shipments = _context.Shipments
            .Where(x => x.DriverId == driver.DriverId
            && x.Status == ShipmentStatus.Committed)
            .Select(x => x.ToDto()).ToList()
        };
    }
}

