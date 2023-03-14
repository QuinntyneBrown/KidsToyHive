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
    [ProducesResponseType(typeof(GetWarehousesResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetWarehousesResponse>> Get()
        => await _meditator.Send(new GetWarehousesRequest());
    [HttpGet("{warehouseId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetWarehouseByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetWarehouseByIdResponse>> GetById([FromRoute] GetWarehouseByIdRequest request)
        => await _meditator.Send(request);
}
