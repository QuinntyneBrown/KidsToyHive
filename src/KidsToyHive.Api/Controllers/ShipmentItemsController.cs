// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.ShipmentItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/shipmentItems")]
public class ShipmentItemsController
{
    private readonly IMediator _meditator;
    public ShipmentItemsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentItemsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentItemsResponse>> Get()
        => await _meditator.Send(new GetShipmentItemsRequest());
    [HttpGet("{shipmentItemId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentItemByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentItemByIdResponse>> GetById([FromRoute] GetShipmentItemByIdRequest request)
        => await _meditator.Send(request);
}

