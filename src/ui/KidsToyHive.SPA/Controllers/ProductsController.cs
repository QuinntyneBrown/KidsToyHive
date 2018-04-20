using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KidsToyHive.SPA.Clients;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductController
    {
        private readonly ProductsClient _client;

        public ProductController(ProductsClient client) => _client = client;

        [HttpPost]
        public async Task<ActionResult<dynamic>> Save(dynamic product)
            => await _client.Save(product);

        [HttpDelete("{productId}")]
        public async Task Remove(int productId)
            => await _client.Remove(productId);

        [AllowAnonymous]
        [HttpGet("{productId}")]
        public async Task<ActionResult<dynamic>> GetById([FromRoute]int productId)
            => await _client.GetById(productId);

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
            => await _client.Get();
    }
}
