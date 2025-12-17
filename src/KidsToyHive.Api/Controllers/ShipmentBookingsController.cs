// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.ShipmentBookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/shipmentBookings")]
public class ShipmentBookingsController
{
    private readonly IMediator _meditator;
    public ShipmentBookingsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentBookingsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentBookingsResponse>> Get()
        => await _meditator.Send(new GetShipmentBookingsRequest());
    [HttpGet("{shipmentBookingId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentBookingByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentBookingByIdResponse>> GetById([FromRoute] GetShipmentBookingByIdRequest request)
        => await _meditator.Send(request);
}

