using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductService.Features.ProductCategories
{
    [Authorize]
    [ApiController]
    [Route("api/productCategories")]
    public class ProductCategoriesController
    {
        private readonly IMediator _mediator;

        public ProductCategoriesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveProductCategoryCommand.Response>> Save(SaveProductCategoryCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{ProductCategory.ProductCategoryId}")]
        public async Task Remove(RemoveProductCategoryCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ProductCategoryId}")]
        public async Task<ActionResult<GetProductCategoryByIdQuery.Response>> GetById([FromRoute]GetProductCategoryByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetProductCategoriesQuery.Response>> Get()
            => await _mediator.Send(new GetProductCategoriesQuery.Request());
    }
}
