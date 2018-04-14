using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IdentityService;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Extensions;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "http://localhost:59041";

        public UsersController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [HttpPost]
        public async Task<ActionResult<CreateUserCommand.Response>> Save(CreateUserCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<CreateUserCommand.Response>($"{_baseUrl}/api/users", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpPut]
        public async Task<ActionResult<UpdateUserCommand.Response>> Update(UpdateUserCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PutAsync<UpdateUserCommand.Response>($"{_baseUrl}/api/users", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<ActionResult<AuthenticateCommand.Response>> SignIn(AuthenticateCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.PostAsync<AuthenticateCommand.Response>($"{_baseUrl}/api/users", new StringContent(JsonConvert.SerializeObject(request)));
        }

        [HttpGet]
        public async Task<ActionResult<GetUsersQuery.Response>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetUsersQuery.Response>($"{_baseUrl}/api/users");
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<GetUserByIdQuery.Response>> GetById(GetUserByIdQuery.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetAsync<GetUserByIdQuery.Response>($"{_baseUrl}/api/users");
        }

        [HttpPost("password")]
        public async Task ChangePassword(UserChangePasswordCommand.Request request)
        {
            var client = _httpClientFactory.CreateClient();

            await client.PostAsync($"{_baseUrl}/api/users", new StringContent(JsonConvert.SerializeObject(request)));
        }
    }
}
