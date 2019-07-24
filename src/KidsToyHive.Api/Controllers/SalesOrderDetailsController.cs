using KidsToyHive.Domain.Features.SalesOrderDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/salesOrderDetails")]
    public class SalesOrderDetailsController
    {
        private readonly IMediator _meditator;

        public SalesOrderDetailsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSalesOrderDetails.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSalesOrderDetails.Response>> Get()
            => await _meditator.Send(new GetSalesOrderDetails.Request());

        [HttpGet("{salesOrderDetailId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSalesOrderDetailById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSalesOrderDetailById.Response>> GetById([FromRoute]GetSalesOrderDetailById.Request request)
            => await _meditator.Send(request);
    }
}
