using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Brands
{
    [Authorize]
    [ApiController]
    [Route("api/brands")]
    public class BrandsController
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateBrandCommand.Response>> Create(CreateBrandCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateBrandCommand.Response>> Update([FromBody]UpdateBrandCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{brandId}")]
        public async Task Remove([FromRoute]RemoveBrandCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{brandId}")]
        public async Task<ActionResult<GetBrandByIdQuery.Response>> GetById([FromRoute]GetBrandByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetBrandsQuery.Response>> Get()
            => await _mediator.Send(new GetBrandsQuery.Request());
    }
}
