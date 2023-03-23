// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.SalesOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/salesOrders")]
public class SalesOrdersController
{
    private readonly IMediator _meditator;
    public SalesOrdersController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetSalesOrdersResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetSalesOrdersResponse>> Get()
        => await _meditator.Send(new GetSalesOrdersRequest());
    [HttpGet("{salesOrderId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetSalesOrderByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetSalesOrderByIdResponse>> GetById([FromRoute] GetSalesOrderByIdRequest request)
        => await _meditator.Send(request);
}

