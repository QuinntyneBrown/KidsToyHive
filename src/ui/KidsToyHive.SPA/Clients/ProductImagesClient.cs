using Infrastructure.Extensions;
using ProductService.Features.ProductImages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace KidsToyHive.SPA.Clients
{
    public class ProductImagesClient
    {
        private HttpClient _client;
        private ILogger<ProductImagesClient> _logger;

        public ProductImagesClient(HttpClient client, ILogger<ProductImagesClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<GetProductImagesQuery.Response> Get()
        {
            try
            {
                return await _client.GetAsync<GetProductImagesQuery.Response>("api/productimages");                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<GetProductImageByIdQuery.Response> GetById(GetProductImageByIdQuery.Request request)
        {
            try
            {
                return await _client.GetAsync<GetProductImageByIdQuery.Response>($"api/productimages/{request.ProductImageId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<SaveProductImageCommand.Response> Save(SaveProductImageCommand.Request request)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(request));

                return await _client.PostAsync<SaveProductImageCommand.Response>("api/productimages",content);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }

        public async Task<GetProductImagesQuery.Response> Remove(RemoveProductImageCommand.Request request)
        {
            try
            {
                return await _client.DeleteAsync<GetProductImagesQuery.Response>($"api/productimages/{request.ProductImage.ProductImageId}");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to ProductImages API {ex.ToString()}");

                throw ex;
            }
        }
    }
}
