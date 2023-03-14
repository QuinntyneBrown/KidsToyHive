using KidsToyHive.Domain.Features.Products;
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
    [ProducesResponseType(typeof(GetProducts.Response), (int)HttpStatusCode.OK)]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
    public async Task<ActionResult<GetProducts.Response>> Get()
        => await _meditator.Send(new GetProducts.Request());
    [HttpGet("{productId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProductById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProductById.Response>> GetById([FromRoute] GetProductById.Request request)
        => await _meditator.Send(request);
}
