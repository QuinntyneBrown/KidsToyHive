using KidsToyHive.Domain.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController
    {
        private readonly IMediator _meditator;

        public UsersController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetUsers.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetUsers.Response>> Get()
            => await _meditator.Send(new GetUsers.Request());

        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetUserById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetUserById.Response>> GetById([FromRoute]GetUserById.Request request)
            => await _meditator.Send(request);

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Authenticate.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Authenticate.Response>> Post([FromBody]Authenticate.Request request)
            => await _meditator.Send(request);
    }
}
