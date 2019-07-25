using KidsToyHive.Domain.Features.ShipmentBookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/shipmentBookings")]
    public class ShipmentBookingsController
    {
        private readonly IMediator _meditator;

        public ShipmentBookingsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetShipmentBookings.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetShipmentBookings.Response>> Get()
            => await _meditator.Send(new GetShipmentBookings.Request());

        [HttpGet("{shipmentBookingId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetShipmentBookingById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetShipmentBookingById.Response>> GetById([FromRoute]GetShipmentBookingById.Request request)
            => await _meditator.Send(request);
    }
}
