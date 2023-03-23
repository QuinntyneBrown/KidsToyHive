// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.SalesOrderDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/salesOrderDetails")]
public class SalesOrderDetailsController
{
    private readonly IMediator _meditator;
    public SalesOrderDetailsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetSalesOrderDetailsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetSalesOrderDetailsResponse>> Get()
        => await _meditator.Send(new GetSalesOrderDetailsRequest());
    [HttpGet("{salesOrderDetailId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetSalesOrderDetailByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetSalesOrderDetailByIdResponse>> GetById([FromRoute] GetSalesOrderDetailByIdRequest request)
        => await _meditator.Send(request);
}

