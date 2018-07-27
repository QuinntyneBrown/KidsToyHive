using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Orders
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrdersController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateOrderCommand.Response>> Create(CreateOrderCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateOrderCommand.Response>> Update([FromBody]UpdateOrderCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{orderId}")]
        public async Task Remove([FromRoute]RemoveOrderCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{orderId}")]
        public async Task<ActionResult<GetOrderByIdQuery.Response>> GetById([FromRoute]GetOrderByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetOrdersQuery.Response>> Get()
            => await _mediator.Send(new GetOrdersQuery.Request());
    }
}
