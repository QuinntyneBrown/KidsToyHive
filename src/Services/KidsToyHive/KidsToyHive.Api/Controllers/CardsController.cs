using KidsToyHive.Domain.Features.Cards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/cards")]
public class CardsController
{
    private readonly IMediator _meditator;
    public CardsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetCardsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCardsResponse>> Get()
        => await _meditator.Send(new GetCardsRequest());
    [HttpGet("{cardId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetCardByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCardByIdResponse>> GetById([FromRoute] GetCardByIdRequest request)
        => await _meditator.Send(request);
}
