using KidsToyHive.Domain.Features.ShipmentItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/shipmentItems")]
public class ShipmentItemsController
{
    private readonly IMediator _meditator;
    public ShipmentItemsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentItems.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentItems.Response>> Get()
        => await _meditator.Send(new GetShipmentItems.Request());
    [HttpGet("{shipmentItemId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentItemById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentItemById.Response>> GetById([FromRoute] GetShipmentItemById.Request request)
        => await _meditator.Send(request);
}
