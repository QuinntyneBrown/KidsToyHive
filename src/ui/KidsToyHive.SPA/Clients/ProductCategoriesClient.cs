using Infrastructure.Extensions;
using ProductService.Features.ProductCategories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace KidsToyHive.SPA.Clients
{
    public class ProductCategoriesClient
    {
        private HttpClient _client;
        private ILogger<ProductCategoriesClient> _logger;

        public ProductCategoriesClient(HttpClient client, ILogger<ProductCategoriesClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<GetProductCategoriesQuery.Response> Get()
        {
            try
            {
                return await _client.GetAsync<GetProductCategoriesQuery.Response>("api/productcategories");                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<GetProductCategoryByIdQuery.Response> GetById(GetProductCategoryByIdQuery.Request request)
        {
            try
            {
                return await _client.GetAsync<GetProductCategoryByIdQuery.Response>($"api/productcategories/{request.ProductCategoryId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<SaveProductCategoryCommand.Response> Save(SaveProductCategoryCommand.Request request)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request));

                return await _client.PostAsync<SaveProductCategoryCommand.Response>("api/productcategories",content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<GetProductCategoriesQuery.Response> Remove(RemoveProductCategoryCommand.Request request)
        {
            try
            {
                return await _client.DeleteAsync<GetProductCategoriesQuery.Response>($"api/productcategories/{request.ProductCategory.ProductCategoryId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }
    }
}
