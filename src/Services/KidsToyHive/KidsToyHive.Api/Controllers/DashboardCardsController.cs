// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.DashboardCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/dashboardCards")]
public class DashboardCardsController
{
    private readonly IMediator _meditator;
    public DashboardCardsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDashboardCardsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDashboardCardsResponse>> Get()
        => await _meditator.Send(new GetDashboardCardsRequest());
    [HttpGet("{dashboardCardId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDashboardCardByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDashboardCardByIdResponse>> GetById([FromRoute] GetDashboardCardByIdRequest request)
        => await _meditator.Send(request);
}

