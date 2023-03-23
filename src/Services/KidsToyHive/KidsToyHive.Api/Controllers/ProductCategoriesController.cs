// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.ProductCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/productCategories")]
public class ProductCategoriesController
{
    private readonly IMediator _meditator;
    public ProductCategoriesController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProductCategoriesResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProductCategoriesResponse>> Get()
        => await _meditator.Send(new GetProductCategoriesRequest());
    [HttpGet("{productCategoryId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProductCategoryByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProductCategoryByIdResponse>> GetById([FromRoute] GetProductCategoryByIdRequest request)
        => await _meditator.Send(request);
}

