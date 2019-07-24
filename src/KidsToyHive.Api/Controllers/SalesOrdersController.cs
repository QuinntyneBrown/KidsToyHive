using KidsToyHive.Domain.Features.SalesOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/salesOrders")]
    public class SalesOrdersController
    {
        private readonly IMediator _meditator;

        public SalesOrdersController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSalesOrders.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSalesOrders.Response>> Get()
            => await _meditator.Send(new GetSalesOrders.Request());

        [HttpGet("{salesOrderId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSalesOrderById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSalesOrderById.Response>> GetById([FromRoute]GetSalesOrderById.Request request)
            => await _meditator.Send(request);
    }
}
