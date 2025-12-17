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

namespace KidsToyHive.Core.Features.ShipmentBookings;

public class GetShipmentBookingsRequest : IRequest<GetShipmentBookingsResponse> { }
public class GetShipmentBookingsResponse
{
    public IEnumerable<ShipmentBookingDto> ShipmentBookings { get; set; }
}
public class GetShipmentBookingsHandler : IRequestHandler<GetShipmentBookingsRequest, GetShipmentBookingsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetShipmentBookingsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentBookingsResponse> Handle(GetShipmentBookingsRequest request, CancellationToken cancellationToken)
        => new GetShipmentBookingsResponse()
        {
            ShipmentBookings = await _context.ShipmentBookings.Select(x => x.ToDto()).ToArrayAsync()
        };
}

