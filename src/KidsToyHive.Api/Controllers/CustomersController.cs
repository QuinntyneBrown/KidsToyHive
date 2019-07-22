using KidsToyHive.Domain.Features.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController
    {
        private readonly IMediator _meditator;

        public CustomersController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCustomers.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCustomers.Response>> Get()
            => await _meditator.Send(new GetCustomers.Request());

        [HttpGet("{customerId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCustomerById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCustomerById.Response>> GetById([FromRoute]GetCustomerById.Request request)
            => await _meditator.Send(request);
    }
}
