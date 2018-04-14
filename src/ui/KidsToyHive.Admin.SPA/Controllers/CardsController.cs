using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DashboardService.Features.Cards;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cards")]
    public class CardsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public CardsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.DashboardServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveCardCommand.Response>> Save(SaveCardCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveCardCommand.Response>($"{_baseUrl}/api/cards", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Card.CardId}")]
        public async Task Remove(RemoveCardCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/cards/{request.Card.CardId}");
        }

        [HttpGet("{CardId}")]
        public async Task<ActionResult<GetCardByIdQuery.Response>> GetById([FromRoute]GetCardByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetCardByIdQuery.Response>($"{_baseUrl}/api/cards/{request.CardId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetCardsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetCardsQuery.Response>($"{_baseUrl}/api/cards");
        }
    }
}
