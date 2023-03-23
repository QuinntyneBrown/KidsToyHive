// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.Brands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/brands")]
public class BrandsController
{
    private readonly IMediator _meditator;
    public BrandsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBrandsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBrandsResponse>> Get()
        => await _meditator.Send(new GetBrandsRequest());
    [HttpGet("{brandId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBrandByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBrandByIdResponse>> GetById([FromRoute] GetBrandByIdRequest request)
        => await _meditator.Send(request);
}

