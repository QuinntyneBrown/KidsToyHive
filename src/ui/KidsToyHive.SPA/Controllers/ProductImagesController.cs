using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductService.Features.ProductImages;
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
        public async Task<ActionResult<SaveProductImageCommand.Response>> Save(SaveProductImageCommand.Request request)
            => await _client.Save(request);

        [HttpDelete("{ProductImage.ProductImageId}")]
        public async Task Remove(RemoveProductImageCommand.Request request)
            => await _client.Remove(request);

        [HttpGet("{ProductImageId}")]
        public async Task<ActionResult<GetProductImageByIdQuery.Response>> GetById([FromRoute]GetProductImageByIdQuery.Request request)
            => await _client.GetById(request);

        [HttpGet]
        public async Task<ActionResult<GetProductImagesQuery.Response>> Get()
            => await _client.Get();
    }
}
