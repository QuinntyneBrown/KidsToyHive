// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController
{
    private readonly IMediator _meditator;
    public ProfilesController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProfilesResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProfilesResponse>> Get()
        => await _meditator.Send(new GetProfilesRequest());
    [HttpGet("{profileId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProfileByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProfileByIdResponse>> GetById([FromRoute] GetProfileByIdRequest request)
        => await _meditator.Send(request);
}

