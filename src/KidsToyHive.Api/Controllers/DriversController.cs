using KidsToyHive.Domain.Features.Drivers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController
    {
        private readonly IMediator _meditator;

        public DriversController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDrivers.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDrivers.Response>> Get()
            => await _meditator.Send(new GetDrivers.Request());

        [HttpGet("{driverId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDriverById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDriverById.Response>> GetById([FromRoute]GetDriverById.Request request)
            => await _meditator.Send(request);

    }
}
