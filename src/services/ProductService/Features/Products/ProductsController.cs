using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductService.Features.Products
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveProductCommand.Response>> Save(SaveProductCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Product.ProductId}")]
        public async Task Remove(RemoveProductCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<GetProductByIdQuery.Response>> GetById([FromRoute]GetProductByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetProductsQuery.Response>> Get()
            => await _mediator.Send(new GetProductsQuery.Request());
    }
}
