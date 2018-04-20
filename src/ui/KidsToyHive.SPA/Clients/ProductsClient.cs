using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Extensions;

namespace KidsToyHive.SPA.Clients
{
    public class ProductsClient: BaseClient<ProductsClient>
    {        
        public ProductsClient(HttpClient client, ILogger<ProductsClient> logger)
            :base(client,logger)
        { }

        public async Task<dynamic> Get()
        {
            try
            {
                return await _client.GetAsync<dynamic>("api/products");                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Products API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> GetById(int productId)
        {
            try
            {
                return await _client.GetAsync<dynamic>($"api/products/{productId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Products API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Save(dynamic product)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new {
                    Product = product
                }));

                return await _client.PostAsync<dynamic>("api/products",content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Products API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Remove(int productId)
        {
            try
            {
                return await _client.DeleteAsync<dynamic>($"api/products/{productId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Products API {ex.ToString()}");

                throw ex;
            }
        }
    }
}
