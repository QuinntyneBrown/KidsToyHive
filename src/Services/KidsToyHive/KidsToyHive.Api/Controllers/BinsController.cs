// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.Bins;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BinsController
{
    private readonly IMediator _meditator;
    public BinsController(IMediator mediator) => _meditator = mediator;
    
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBinsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBinsResponse>> Get()
        => await _meditator.Send(new GetBinsRequest());

    [HttpGet("{binId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBinByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBinByIdResponse>> GetById([FromRoute] GetBinByIdRequest request)
        => await _meditator.Send(request);
}

