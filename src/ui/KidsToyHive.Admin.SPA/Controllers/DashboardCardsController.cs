using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DashboardService.Features.DashboardCards;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/dashboardcards")]
    public class DashboardCardsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public DashboardCardsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.DashboardServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveDashboardCardCommand.Response>> Save(SaveDashboardCardCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveDashboardCardCommand.Response>($"{_baseUrl}/api/dashboardcards", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{DashboardCard.DashboardCardId}")]
        public async Task Remove(RemoveDashboardCardCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/dashboardcards/{request.DashboardCard.DashboardCardId}");
        }

        [HttpGet("{DashboardCardId}")]
        public async Task<ActionResult<GetDashboardCardByIdQuery.Response>> GetById([FromRoute]GetDashboardCardByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetDashboardCardByIdQuery.Response>($"{_baseUrl}/api/dashboardcards/{request.DashboardCardId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetDashboardCardsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetDashboardCardsQuery.Response>($"{_baseUrl}/api/dashboardcards");
        }
    }
}
