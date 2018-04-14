using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DigitalAssetService.Features.DigitalAssets;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/digitalassets")]
    public class DigitalAssetsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public DigitalAssetsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.DigitalAssetServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveDigitalAssetCommand.Response>> Save(SaveDigitalAssetCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveDigitalAssetCommand.Response>($"{_baseUrl}/api/digitalassets", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{DigitalAsset.DigitalAssetId}")]
        public async Task Remove(RemoveDigitalAssetCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/digitalassets/{request.DigitalAsset.DigitalAssetId}");
        }

        [HttpGet("{DigitalAssetId}")]
        public async Task<ActionResult<GetDigitalAssetByIdQuery.Response>> GetById([FromRoute]GetDigitalAssetByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetDigitalAssetByIdQuery.Response>($"{_baseUrl}/api/digitalassets/{request.DigitalAssetId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetDigitalAssetsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetDigitalAssetsQuery.Response>($"{_baseUrl}/api/digitalassets");
        }
    }
}
