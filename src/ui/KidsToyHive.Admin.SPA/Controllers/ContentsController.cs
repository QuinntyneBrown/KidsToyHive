using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContentService.Features.Contents;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/contents")]
    public class ContentsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public ContentsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.ContentServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveContentCommand.Response>> Save(SaveContentCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveContentCommand.Response>($"{_baseUrl}/api/contents", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Content.ContentId}")]
        public async Task Remove(RemoveContentCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/contents/{request.Content.ContentId}");
        }

        [HttpGet("{ContentId}")]
        public async Task<ActionResult<GetContentByIdQuery.Response>> GetById([FromRoute]GetContentByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetContentByIdQuery.Response>($"{_baseUrl}/api/contents/{request.ContentId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetContentsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetContentsQuery.Response>($"{_baseUrl}/api/contents");
        }
    }
}
