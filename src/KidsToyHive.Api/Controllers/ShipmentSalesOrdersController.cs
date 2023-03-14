using KidsToyHive.Domain.Features.ShipmentSalesOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/shipmentSalesOrders")]
public class ShipmentSalesOrdersController
{
    private readonly IMediator _meditator;
    public ShipmentSalesOrdersController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentSalesOrdersResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentSalesOrdersResponse>> Get()
        => await _meditator.Send(new GetShipmentSalesOrdersRequest());
    [HttpGet("{shipmentSalesOrderId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentSalesOrderByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentSalesOrderByIdResponse>> GetById([FromRoute] GetShipmentSalesOrderByIdRequest request)
        => await _meditator.Send(request);
}
