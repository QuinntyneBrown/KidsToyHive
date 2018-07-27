using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Orders
{
    [Authorize]
    [ApiController]
    [Route("api/orderItems")]
    public class OrderItemsController
    {
        private readonly IMediator _mediator;

        public OrderItemsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateOrderItemCommand.Response>> Create(CreateOrderItemCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateOrderItemCommand.Response>> Update([FromBody]UpdateOrderItemCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{orderItemId}")]
        public async Task Remove([FromRoute]RemoveOrderItemCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{orderItemId}")]
        public async Task<ActionResult<GetOrderItemByIdQuery.Response>> GetById([FromRoute]GetOrderItemByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetOrderItemsQuery.Response>> Get()
            => await _mediator.Send(new GetOrderItemsQuery.Request());
    }
}
