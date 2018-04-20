using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<dynamic>> Save(dynamic productCategory)
            => await _client.Save(productCategory);

        [HttpDelete("{productCategoryId}")]
        public async Task Remove(int productCategoryId)
            => await _client.Remove(productCategoryId);

        [AllowAnonymous]
        [HttpGet("{productCategoryId}")]
        public async Task<ActionResult<dynamic>> GetById([FromRoute]int productCategoryId)
            => await _client.GetById(productCategoryId);

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
            => await _client.Get();
    }
}
