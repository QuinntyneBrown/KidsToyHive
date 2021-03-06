using KidsToyHive.Domain.Features.Cards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardsController
    {
        private readonly IMediator _meditator;

        public CardsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCards.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCards.Response>> Get()
            => await _meditator.Send(new GetCards.Request());

        [HttpGet("{cardId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCardById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCardById.Response>> GetById([FromRoute]GetCardById.Request request)
            => await _meditator.Send(request);
    }
}
