using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KidsToyHive.Domain.Features.Geolocation;

namespace KidsToyHive.Api.Controllers
{
    [Route("api/geolocation")]
    public class GeolocationController : Controller
    {
        protected readonly IMediator _mediator;

        public GeolocationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Route("getAddress")]
        [AllowAnonymous]
        [HttpGet]
        [Produces(typeof(GetAddressFromLatitudeAndLongitude.Response))]
        public async Task<IActionResult> GetAddress([FromRoute]GetAddressFromLatitudeAndLongitude.Request request)
            => Ok(await _mediator.Send(request));
    }
}
