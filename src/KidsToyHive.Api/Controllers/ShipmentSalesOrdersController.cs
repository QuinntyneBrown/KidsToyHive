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
    [ProducesResponseType(typeof(GetShipmentSalesOrders.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentSalesOrders.Response>> Get()
        => await _meditator.Send(new GetShipmentSalesOrders.Request());
    [HttpGet("{shipmentSalesOrderId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetShipmentSalesOrderById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetShipmentSalesOrderById.Response>> GetById([FromRoute] GetShipmentSalesOrderById.Request request)
        => await _meditator.Send(request);
}
