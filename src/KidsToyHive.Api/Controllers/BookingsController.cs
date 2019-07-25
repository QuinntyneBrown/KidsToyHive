using KidsToyHive.Domain.Features.Bookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController
    {
        private readonly IMediator _meditator;

        public BookingsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBookings.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBookings.Response>> Get()
            => await _meditator.Send(new GetBookings.Request());

        [HttpGet("{bookingId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBookingById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBookingById.Response>> GetById([FromRoute]GetBookingById.Request request)
            => await _meditator.Send(request);
    }
}
