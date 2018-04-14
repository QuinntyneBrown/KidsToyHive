using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductService.Features.ProductImages;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/productimages")]
    public class ProductImagesController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public ProductImagesController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.ProductServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveProductImageCommand.Response>> Save(SaveProductImageCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveProductImageCommand.Response>($"{_baseUrl}/api/productimages", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{ProductImage.ProductImageId}")]
        public async Task Remove(RemoveProductImageCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/productimages/{request.ProductImage.ProductImageId}");
        }

        [HttpGet("{ProductImageId}")]
        public async Task<ActionResult<GetProductImageByIdQuery.Response>> GetById([FromRoute]GetProductImageByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetProductImageByIdQuery.Response>($"{_baseUrl}/api/productimages/{request.ProductImageId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetProductImagesQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetProductImagesQuery.Response>($"{_baseUrl}/api/productimages");
        }
    }
}
