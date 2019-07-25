using KidsToyHive.Domain.Features.BookingDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/bookingDetails")]
    public class BookingDetailsController
    {
        private readonly IMediator _meditator;

        public BookingDetailsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBookingDetails.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBookingDetails.Response>> Get()
            => await _meditator.Send(new GetBookingDetails.Request());

        [HttpGet("{bookingDetailId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBookingDetailById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBookingDetailById.Response>> GetById([FromRoute]GetBookingDetailById.Request request)
            => await _meditator.Send(request);
    }
}
