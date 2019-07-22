using KidsToyHive.Domain.Features.ProductCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/productCategories")]
    public class ProductCategoriesController
    {
        private readonly IMediator _meditator;

        public ProductCategoriesController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProductCategories.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProductCategories.Response>> Get()
            => await _meditator.Send(new GetProductCategories.Request());

        [HttpGet("{productCategoryId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProductCategoryById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProductCategoryById.Response>> GetById([FromRoute]GetProductCategoryById.Request request)
            => await _meditator.Send(request);
    }
}
