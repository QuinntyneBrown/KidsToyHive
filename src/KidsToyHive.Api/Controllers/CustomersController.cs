using KidsToyHive.Domain.Features.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController
{
    private readonly IMediator _meditator;
    public CustomersController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetCustomersResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCustomersResponse>> Get()
        => await _meditator.Send(new GetCustomersRequest());
    [HttpGet("{customerId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetCustomerByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetCustomerByIdResponse>> GetById([FromRoute] GetCustomerByIdRequest request)
        => await _meditator.Send(request);
}
