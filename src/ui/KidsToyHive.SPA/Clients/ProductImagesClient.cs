using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace KidsToyHive.SPA.Clients
{
    public class ProductImagesClient: BaseClient<ProductImagesClient>
    {        
        public ProductImagesClient(HttpClient client, ILogger<ProductImagesClient> logger)
            :base(client,logger)
        { }

        public async Task<dynamic> Get()
        {
            try
            {
                return await _client.GetAsync<dynamic>("api/productimages");                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> GetById(int productImageId)
        {
            try
            {
                return await _client.GetAsync<dynamic>($"api/productimages/{productImageId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Save(dynamic productImage)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new {
                    ProductImage = productImage
                }));

                return await _client.PostAsync<dynamic>("api/productimages",content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Remove(int productImageId)
        {
            try
            {
                return await _client.DeleteAsync<dynamic>($"api/productimages/{productImageId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }
    }
}
