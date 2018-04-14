using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContactService.Features.ContactRequests;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/contactrequests")]
    public class ContactRequestsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public ContactRequestsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.ContactServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveContactRequestCommand.Response>> Save(SaveContactRequestCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveContactRequestCommand.Response>($"{_baseUrl}/api/contactrequests", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{ContactRequest.ContactRequestId}")]
        public async Task Remove(RemoveContactRequestCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/contactrequests/{request.ContactRequest.ContactRequestId}");
        }

        [HttpGet("{ContactRequestId}")]
        public async Task<ActionResult<GetContactRequestByIdQuery.Response>> GetById([FromRoute]GetContactRequestByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetContactRequestByIdQuery.Response>($"{_baseUrl}/api/contactrequests/{request.ContactRequestId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetContactRequestsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetContactRequestsQuery.Response>($"{_baseUrl}/api/contactrequests");
        }
    }
}
