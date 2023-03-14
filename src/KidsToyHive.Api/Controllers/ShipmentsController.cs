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
    [ProducesResponseType(typeof(GetShipmentsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentsResponse>> Get()
        => await _meditator.Send(new GetShipmentsRequest());
    [HttpGet("incomplete")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetIncompleteShipmentsResponse>> GetIncomplete()
        => await _meditator.Send(new GetIncompleteShipmentsRequest());
    [HttpGet("{shipmentId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentByIdResponse>> GetById([FromRoute] GetShipmentByIdRequest request)
        => await _meditator.Send(request);
}
