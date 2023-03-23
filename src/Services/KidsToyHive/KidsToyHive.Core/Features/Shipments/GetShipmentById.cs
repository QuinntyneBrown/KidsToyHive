// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Shipments;

public class GetShipmentByIdRequest : IRequest<GetShipmentByIdResponse>
{
    public Guid ShipmentId { get; set; }
}
public class GetShipmentByIdResponse
{
    public ShipmentDto Shipment { get; set; }
}
public class GetShipmentByIdHandler : IRequestHandler<GetShipmentByIdRequest, GetShipmentByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetShipmentByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentByIdResponse> Handle(GetShipmentByIdRequest request, CancellationToken cancellationToken)
        => new GetShipmentByIdResponse()
        {
            Shipment = (await _context.Shipments.FindAsync(request.ShipmentId)).ToDto()
        };
}

