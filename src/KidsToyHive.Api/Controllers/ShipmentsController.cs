using KidsToyHive.Domain.Features.Shipments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/shipments")]
public class ShipmentsController
{
    private readonly IMediator _meditator;
    public ShipmentsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipments.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipments.Response>> Get()
        => await _meditator.Send(new GetShipments.Request());
    [HttpGet("incomplete")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipments.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetIncompleteShipments.Response>> GetIncomplete()
        => await _meditator.Send(new GetIncompleteShipments.Request());
    [HttpGet("{shipmentId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentById.Response>> GetById([FromRoute] GetShipmentById.Request request)
        => await _meditator.Send(request);
}
