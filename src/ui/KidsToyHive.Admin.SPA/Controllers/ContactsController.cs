using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContactService.Features.Contacts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public ContactsController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.ContactServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveContactCommand.Response>> Save(SaveContactCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveContactCommand.Response>($"{_baseUrl}/api/contacts", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Contact.ContactId}")]
        public async Task Remove(RemoveContactCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/contacts/{request.Contact.ContactId}");
        }

        [HttpGet("{ContactId}")]
        public async Task<ActionResult<GetContactByIdQuery.Response>> GetById([FromRoute]GetContactByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetContactByIdQuery.Response>($"{_baseUrl}/api/contacts/{request.ContactId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetContactsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetContactsQuery.Response>($"{_baseUrl}/api/contacts");
        }
    }
}
