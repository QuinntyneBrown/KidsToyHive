using KidsToyHive.Domain.Features.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController
    {
        private readonly IMediator _meditator;

        public OrdersController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOrders.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetOrders.Response>> Get()
            => await _meditator.Send(new GetOrders.Request());

        [HttpGet("{orderId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetOrderById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetOrderById.Response>> GetById([FromRoute]GetOrderById.Request request)
            => await _meditator.Send(request);
    }
}
