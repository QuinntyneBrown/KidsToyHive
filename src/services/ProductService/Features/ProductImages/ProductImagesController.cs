using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductService.Features.ProductImages
{
    [Authorize]
    [ApiController]
    [Route("api/productImages")]
    public class ProductImagesController
    {
        private readonly IMediator _mediator;

        public ProductImagesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveProductImageCommand.Response>> Save(SaveProductImageCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{ProductImage.ProductImageId}")]
        public async Task Remove(RemoveProductImageCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ProductImageId}")]
        public async Task<ActionResult<GetProductImageByIdQuery.Response>> GetById([FromRoute]GetProductImageByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetProductImagesQuery.Response>> Get()
            => await _mediator.Send(new GetProductImagesQuery.Request());
    }
}
