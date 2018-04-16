using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductService.Features.Brands
{
    [Authorize]
    [ApiController]
    [Route("api/brands")]
    public class BrandsController
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveBrandCommand.Response>> Save(SaveBrandCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Brand.BrandId}")]
        public async Task Remove(RemoveBrandCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{BrandId}")]
        public async Task<ActionResult<GetBrandByIdQuery.Response>> GetById([FromRoute]GetBrandByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetBrandsQuery.Response>> Get()
            => await _mediator.Send(new GetBrandsQuery.Request());
    }
}
