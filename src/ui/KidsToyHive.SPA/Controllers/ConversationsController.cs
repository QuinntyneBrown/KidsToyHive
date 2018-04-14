using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ChatService.Features.Conversations;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/conversations")]
    public class ConversationsController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "https://localhost:44395";

        public ConversationsController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [HttpPost]
        public async Task<ActionResult<SaveConversationCommand.Response>> Save(SaveConversationCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveConversationCommand.Response>($"{_baseUrl}/api/conversations", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpDelete("{Conversation.ConversationId}")]
        public async Task Remove(RemoveConversationCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.DeleteAsync($"{_baseUrl}/api/conversations/{request.Conversation.ConversationId}");
        }

        [HttpGet("{ConversationId}")]
        public async Task<ActionResult<GetConversationByIdQuery.Response>> GetById([FromRoute]GetConversationByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetConversationByIdQuery.Response>($"{_baseUrl}/api/conversations/{request.ConversationId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetConversationsQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetConversationsQuery.Response>($"{_baseUrl}/api/conversations");
        }
    }
}