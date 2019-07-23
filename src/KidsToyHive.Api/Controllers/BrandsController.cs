using KidsToyHive.Domain.Features.Brands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController
    {
        private readonly IMediator _meditator;

        public BrandsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBrands.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBrands.Response>> Get()
            => await _meditator.Send(new GetBrands.Request());

        [AllowAnonymous]
        [HttpGet("{brandId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBrandById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBrandById.Response>> GetById([FromRoute]GetBrandById.Request request)
            => await _meditator.Send(request);
    }
}
