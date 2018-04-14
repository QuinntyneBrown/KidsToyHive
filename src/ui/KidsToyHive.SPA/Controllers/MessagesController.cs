using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ChatService.Features.Messages;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/messages")]
    public class MessagesController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "https://localhost:44395";

        public MessagesController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [HttpPost]
        public async Task<ActionResult<SaveMessageCommand.Response>> Save(SaveMessageCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveMessageCommand.Response>($"{_baseUrl}/api/messages", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Message.MessageId}")]
        public async Task Remove(RemoveMessageCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/messages/{request.Message.MessageId}");
        }

        [HttpGet("{MessageId}")]
        public async Task<ActionResult<GetMessageByIdQuery.Response>> GetById([FromRoute]GetMessageByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetMessageByIdQuery.Response>($"{_baseUrl}/api/messages/{request.MessageId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetMessagesQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetMessagesQuery.Response>($"{_baseUrl}/api/messages");
        }
    }
}