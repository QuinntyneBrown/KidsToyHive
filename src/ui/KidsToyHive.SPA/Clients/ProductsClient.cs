using Infrastructure.Extensions;
using ProductService.Features.Products;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace KidsToyHive.SPA.Clients
{
    public class ProductsClient
    {
        private HttpClient _client;
        private ILogger<ProductsClient> _logger;

        public ProductsClient(HttpClient client, ILogger<ProductsClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<GetProductsQuery.Response> Get()
        {
            try
            {
                return await _client.GetAsync<GetProductsQuery.Response>("api/products");                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to products API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<GetProductByIdQuery.Response> GetById(GetProductByIdQuery.Request request)
        {
            try
            {
                return await _client.GetAsync<GetProductByIdQuery.Response>($"api/products/{request.ProductId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to products API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<SaveProductCommand.Response> Save(SaveProductCommand.Request request)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request));

                return await _client.PostAsync<SaveProductCommand.Response>("api/products",content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to products API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<GetProductsQuery.Response> Remove(RemoveProductCommand.Request request)
        {
            try
            {
                return await _client.DeleteAsync<GetProductsQuery.Response>($"api/products/{request.Product.ProductId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to products API {ex.ToString()}");

                throw ex;
            }
        }
    }
}
