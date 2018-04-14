using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductService.Features.ProductCategories;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/productcategories")]
    public class ProductCategoriesController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public ProductCategoriesController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.ProductServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveProductCategoryCommand.Response>> Save(SaveProductCategoryCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveProductCategoryCommand.Response>($"{_baseUrl}/api/productcategories", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{ProductCategory.ProductCategoryId}")]
        public async Task Remove(RemoveProductCategoryCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/productcategories/{request.ProductCategory.ProductCategoryId}");
        }

        [HttpGet("{ProductCategoryId}")]
        public async Task<ActionResult<GetProductCategoryByIdQuery.Response>> GetById([FromRoute]GetProductCategoryByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetProductCategoryByIdQuery.Response>($"{_baseUrl}/api/productcategories/{request.ProductCategoryId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetProductCategoriesQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetProductCategoriesQuery.Response>($"{_baseUrl}/api/productcategories");
        }
    }
}
