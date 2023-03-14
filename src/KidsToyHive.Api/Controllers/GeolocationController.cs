using KidsToyHive.Domain.Features.Geolocation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[Route("api/geolocation")]
public class GeolocationController : Controller
{
    protected readonly IMediator _mediator;
    public GeolocationController(IMediator mediator)
        => _mediator = mediator;
    [Route("getAddress")]
    [AllowAnonymous]
    [HttpGet]
    [Produces(typeof(GetAddressFromLatitudeAndLongitudeResponse))]
    public async Task<IActionResult> GetAddress([FromRoute] GetAddressFromLatitudeAndLongitudeRequest request)
        => Ok(await _mediator.Send(request));
}
