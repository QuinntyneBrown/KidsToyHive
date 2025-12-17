// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/products")]
public class ProductsController
{
    private readonly IMediator _meditator;
    public ProductsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProductsResponse), (int)HttpStatusCode.OK)]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
    public async Task<ActionResult<GetProductsResponse>> Get()
        => await _meditator.Send(new GetProductsRequest());
    [HttpGet("{productId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProductByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProductByIdResponse>> GetById([FromRoute] GetProductByIdRequest request)
        => await _meditator.Send(request);
}

