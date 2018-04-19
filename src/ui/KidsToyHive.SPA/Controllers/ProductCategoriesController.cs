using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductService.Features.ProductCategories;
using KidsToyHive.SPA.Clients;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/productcategories")]
    public class ProductCategoryController
    {
        private readonly ProductCategoriesClient _client;

        public ProductCategoryController(ProductCategoriesClient client) => _client = client;

        [HttpPost]
        public async Task<ActionResult<SaveProductCategoryCommand.Response>> Save(SaveProductCategoryCommand.Request request)
            => await _client.Save(request);

        [HttpDelete("{ProductCategory.ProductCategoryId}")]
        public async Task Remove(RemoveProductCategoryCommand.Request request)
            => await _client.Remove(request);

        [HttpGet("{ProductCategoryId}")]
        public async Task<ActionResult<GetProductCategoryByIdQuery.Response>> GetById([FromRoute]GetProductCategoryByIdQuery.Request request)
            => await _client.GetById(request);

        [HttpGet]
        public async Task<ActionResult<GetProductCategoriesQuery.Response>> Get()
            => await _client.Get();
    }
}
