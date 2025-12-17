// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentBookings;

public class GetShipmentBookingByIdRequest : IRequest<GetShipmentBookingByIdResponse>
{
    public Guid ShipmentBookingId { get; set; }
}
public class GetShipmentBookingByIdResponse
{
    public ShipmentBookingDto ShipmentBooking { get; set; }
}
public class GetShipmentBookingByIdHandler : IRequestHandler<GetShipmentBookingByIdRequest, GetShipmentBookingByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetShipmentBookingByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentBookingByIdResponse> Handle(GetShipmentBookingByIdRequest request, CancellationToken cancellationToken)
        => new GetShipmentBookingByIdResponse()
        {
            ShipmentBooking = (await _context.ShipmentBookings.FindAsync(request.ShipmentBookingId)).ToDto()
        };
}

