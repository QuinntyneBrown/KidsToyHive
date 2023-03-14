using KidsToyHive.Domain.Features.Warehouses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/warehouses")]
public class WarehousesController
{
    private readonly IMediator _meditator;
    public WarehousesController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetWarehouses.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetWarehouses.Response>> Get()
        => await _meditator.Send(new GetWarehouses.Request());
    [HttpGet("{warehouseId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetWarehouseById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetWarehouseById.Response>> GetById([FromRoute] GetWarehouseById.Request request)
        => await _meditator.Send(request);
}
