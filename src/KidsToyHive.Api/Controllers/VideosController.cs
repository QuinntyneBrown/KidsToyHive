// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.Videos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/videos")]
public class VideosController
{
    private readonly IMediator _meditator;
    public VideosController(IMediator mediator) => _meditator = mediator;
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetVideosResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetVideosResponse>> Get()
        => await _meditator.Send(new GetVideosRequest());
    [AllowAnonymous]
    [HttpGet("{videoId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetVideoByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetVideoByIdResponse>> GetById([FromRoute] GetVideoByIdRequest request)
        => await _meditator.Send(request);
}

