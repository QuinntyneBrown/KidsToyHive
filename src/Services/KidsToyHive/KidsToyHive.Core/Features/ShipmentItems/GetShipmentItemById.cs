// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ShipmentItems;

public class GetShipmentItemByIdRequest : IRequest<GetShipmentItemByIdResponse>
{
    public Guid ShipmentItemId { get; set; }
}
public class GetShipmentItemByIdResponse
{
    public ShipmentItemDto ShipmentItem { get; set; }
}
public class GetShipmentItemByIdHandler : IRequestHandler<GetShipmentItemByIdRequest, GetShipmentItemByIdResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetShipmentItemByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetShipmentItemByIdResponse> Handle(GetShipmentItemByIdRequest request, CancellationToken cancellationToken)
        => new GetShipmentItemByIdResponse()
        {
            ShipmentItem = (await _context.ShipmentItems.FindAsync(request.ShipmentItemId)).ToDto()
        };
}

