using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace KidsToyHive.SPA.Clients
{
    public class BrandsClient: BaseClient<BrandsClient>
    {        
        public BrandsClient(HttpClient client, ILogger<BrandsClient> logger)
            :base(client,logger)
        { }

        public async Task<dynamic> Get()
        {
            try
            {
                return await _client.GetAsync<dynamic>("api/brands");                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Brands API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> GetById(int brandId)
        {
            try
            {
                return await _client.GetAsync<dynamic>($"api/brands/{brandId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Brands API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Save(dynamic brand)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new {
                    Brand = brand
                }));

                return await _client.PostAsync<dynamic>("api/brands",content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Brands API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Remove(int brandId)
        {
            try
            {
                return await _client.DeleteAsync<dynamic>($"api/brands/{brandId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to Brands API {ex.ToString()}");

                throw ex;
            }
        }
    }
}
