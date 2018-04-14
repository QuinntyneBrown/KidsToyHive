using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdentityService.Features.Users;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KidsToyHive.Admin.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public UsersController(IHttpClientFactory httpClientFactory, IOptions<ClusterSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value.IdentityServiceBaseUrl;
        }

        [HttpPost]
        public async Task<ActionResult<SaveUserCommand.Response>> Save(SaveUserCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<SaveUserCommand.Response>($"{_baseUrl}/api/users", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpPost("token")]
        public async Task<ActionResult<AuthenticateCommand.Response>> Token(AuthenticateCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<AuthenticateCommand.Response>($"{_baseUrl}/api/users/token", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<GetUserByIdQuery.Response>> GetById([FromRoute]GetUserByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetUserByIdQuery.Response>($"{_baseUrl}/api/users/{request.UserId}");
        }

        [HttpGet]
        public async Task<ActionResult<GetUsersQuery.Response>> Get()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetUsersQuery.Response>($"{_baseUrl}/api/users");
        }
    }
}
