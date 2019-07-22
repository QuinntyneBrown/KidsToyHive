using KidsToyHive.Domain.Features.OrderItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/orderItems")]
    public class OrderItemsController
    {
        private readonly IMediator _meditator;

        public OrderItemsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOrderItems.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetOrderItems.Response>> Get()
            => await _meditator.Send(new GetOrderItems.Request());

        [HttpGet("{orderItemId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOrderItemById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetOrderItemById.Response>> GetById([FromRoute]GetOrderItemById.Request request)
            => await _meditator.Send(request);
    }
}
