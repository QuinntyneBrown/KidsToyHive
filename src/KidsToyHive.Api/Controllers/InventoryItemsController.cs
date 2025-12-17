// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.InventoryItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/inventoryItems")]
public class InventoryItemsController
{
    private readonly IMediator _meditator;
    public InventoryItemsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetInventoryItemsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetInventoryItemsResponse>> Get()
        => await _meditator.Send(new GetInventoryItemsRequest());
    [HttpGet("{inventoryItemId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetInventoryItemByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetInventoryItemByIdResponse>> GetById([FromRoute] GetInventoryItemByIdRequest request)
        => await _meditator.Send(request);
}

