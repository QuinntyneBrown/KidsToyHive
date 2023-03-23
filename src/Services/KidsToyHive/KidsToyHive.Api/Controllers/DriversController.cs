// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.Drivers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriversController
{
    private readonly IMediator _meditator;
    public DriversController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDriversResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDriversResponse>> Get()
        => await _meditator.Send(new GetDriversRequest());
    [HttpGet("{driverId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDriverByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDriverByIdResponse>> GetById([FromRoute] GetDriverByIdRequest request)
        => await _meditator.Send(request);
}

