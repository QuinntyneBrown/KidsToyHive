using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace KidsToyHive.SPA.Clients
{
    public class ProductCategoriesClient: BaseClient<ProductCategoriesClient>
    {        
        public ProductCategoriesClient(HttpClient client, ILogger<ProductCategoriesClient> logger)
            :base(client,logger)
        { }

        public async Task<dynamic> Get()
        {
            try
            {
                return await _client.GetAsync<dynamic>("api/productcategories");                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> GetById(int productCategoryId)
        {
            try
            {
                return await _client.GetAsync<dynamic>($"api/productcategories/{productCategoryId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Save(dynamic productCategory)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new {
                    ProductCategory = productCategory
                }));

                return await _client.PostAsync<dynamic>("api/productcategories",content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<dynamic> Remove(int productCategoryId)
        {
            try
            {
                return await _client.DeleteAsync<dynamic>($"api/productcategories/{productCategoryId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductCategories API {ex.ToString()}");

                throw ex;
            }
        }
    }
}
