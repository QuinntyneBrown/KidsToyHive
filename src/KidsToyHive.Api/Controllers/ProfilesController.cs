using KidsToyHive.Domain.Features.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController
    {
        private readonly IMediator _meditator;

        public ProfilesController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProfiles.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProfiles.Response>> Get()
            => await _meditator.Send(new GetProfiles.Request());

        [HttpGet("{profileId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProfileById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProfileById.Response>> GetById([FromRoute]GetProfileById.Request request)
            => await _meditator.Send(request);
    }
}
