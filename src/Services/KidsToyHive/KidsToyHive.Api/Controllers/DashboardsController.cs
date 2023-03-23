// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.Dashboards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/dashboards")]
public class DashboardsController
{
    private readonly IMediator _meditator;
    public DashboardsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDashboardsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDashboardsResponse>> Get()
        => await _meditator.Send(new GetDashboardsRequest());
    [HttpGet("{dashboardId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDashboardByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDashboardByIdResponse>> GetById([FromRoute] GetDashboardByIdRequest request)
        => await _meditator.Send(request);
}

