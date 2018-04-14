using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductService.Features.Products;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public ProductsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.ProductServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveProductCommand.Response>> Save(SaveProductCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveProductCommand.Response>($"{_baseUrl}/api/products", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Product.ProductId}")]
        public async Task Remove(RemoveProductCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/products/{request.Product.ProductId}");
        }

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<GetProductByIdQuery.Response>> GetById([FromRoute]GetProductByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetProductByIdQuery.Response>($"{_baseUrl}/api/products/{request.ProductId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetProductsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetProductsQuery.Response>($"{_baseUrl}/api/products");
        }
    }
}
