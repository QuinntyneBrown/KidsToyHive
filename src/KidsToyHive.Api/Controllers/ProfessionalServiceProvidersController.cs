using KidsToyHive.Domain.Features.ProfessionalServiceProviders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/professionalServiceProviders")]
    public class ProfessionalServiceProvidersController
    {
        private readonly IMediator _meditator;

        public ProfessionalServiceProvidersController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProfessionalServiceProviders.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProfessionalServiceProviders.Response>> Get()
            => await _meditator.Send(new GetProfessionalServiceProviders.Request());

        [HttpGet("{professionalServiceProviderId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProfessionalServiceProviderById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProfessionalServiceProviderById.Response>> GetById([FromRoute]GetProfessionalServiceProviderById.Request request)
            => await _meditator.Send(request);

        [AllowAnonymous]
        [HttpGet("name/{fullName}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProfessionalServiceProviderById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProfessionalServiceProviderByName.Response>> GetByName([FromRoute]GetProfessionalServiceProviderByName.Request request)
            => await _meditator.Send(request);

    }
}
