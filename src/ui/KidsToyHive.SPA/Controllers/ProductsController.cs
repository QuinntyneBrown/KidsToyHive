using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductService.Features.Products;
using KidsToyHive.SPA.Clients;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController
    {
        private readonly ProductsClient _client;

        public ProductsController(ProductsClient client) => _client = client;

        [HttpPost]
        public async Task<ActionResult<SaveProductCommand.Response>> Save(SaveProductCommand.Request request)
            => await _client.Save(request);

        [HttpDelete("{Product.ProductId}")]
        public async Task Remove(RemoveProductCommand.Request request)
            => await _client.Remove(request);

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<GetProductByIdQuery.Response>> GetById([FromRoute]GetProductByIdQuery.Request request)
            => await _client.GetById(request);

        [HttpGet]
        public async Task<ActionResult<GetProductsQuery.Response>> Get()
            => await _client.Get();
    }
}
