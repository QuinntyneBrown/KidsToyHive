using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KidsToyHive.SPA.Clients;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/productimages")]
    public class ProductImageController
    {
        private readonly ProductImagesClient _client;

        public ProductImageController(ProductImagesClient client) => _client = client;

        [HttpPost]
        public async Task<ActionResult<dynamic>> Save(dynamic productImage)
            => await _client.Save(productImage);

        [HttpDelete("{productImageId}")]
        public async Task Remove(int productImageId)
            => await _client.Remove(productImageId);

        [AllowAnonymous]
        [HttpGet("{productImageId}")]
        public async Task<ActionResult<dynamic>> GetById([FromRoute]int productImageId)
            => await _client.GetById(productImageId);

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
            => await _client.Get();
    }
}
