using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DashboardService.Features.Dashboards;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/dashboards")]
    public class DashboardsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public DashboardsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.DashboardServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveDashboardCommand.Response>> Save(SaveDashboardCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveDashboardCommand.Response>($"{_baseUrl}/api/dashboards", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Dashboard.DashboardId}")]
        public async Task Remove(RemoveDashboardCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/dashboards/{request.Dashboard.DashboardId}");
        }

        [HttpGet("{DashboardId}")]
        public async Task<ActionResult<GetDashboardByIdQuery.Response>> GetById([FromRoute]GetDashboardByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetDashboardByIdQuery.Response>($"{_baseUrl}/api/dashboards/{request.DashboardId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetDashboardsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetDashboardsQuery.Response>($"{_baseUrl}/api/dashboards");
        }
    }
}
