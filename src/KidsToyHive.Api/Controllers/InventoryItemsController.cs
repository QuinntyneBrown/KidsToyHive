using KidsToyHive.Domain.Features.InventoryItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/inventoryItems")]
public class InventoryItemsController
{
    private readonly IMediator _meditator;
    public InventoryItemsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetInventoryItems.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetInventoryItems.Response>> Get()
        => await _meditator.Send(new GetInventoryItems.Request());
    [HttpGet("{inventoryItemId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetInventoryItemById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetInventoryItemById.Response>> GetById([FromRoute] GetInventoryItemById.Request request)
        => await _meditator.Send(request);
}
