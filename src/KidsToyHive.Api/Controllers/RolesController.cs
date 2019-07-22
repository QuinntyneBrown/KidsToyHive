using KidsToyHive.Domain.Features.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController
    {
        private readonly IMediator _meditator;

        public RolesController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRoles.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetRoles.Response>> Get()
            => await _meditator.Send(new GetRoles.Request());

        [HttpGet("{roleId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetRoleById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetRoleById.Response>> GetById([FromRoute]GetRoleById.Request request)
            => await _meditator.Send(request);

    }
}
