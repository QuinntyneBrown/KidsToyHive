using KidsToyHive.Domain.Features.Contacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController
{
    private readonly IMediator _meditator;
    public ContactsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetContactsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetContactsResponse>> Get()
        => await _meditator.Send(new GetContactsRequest());
    [HttpGet("{contactId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetContactByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetContactByIdResponse>> GetById([FromRoute] GetContactByIdRequest request)
        => await _meditator.Send(request);
}
