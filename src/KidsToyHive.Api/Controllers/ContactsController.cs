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
    [ProducesResponseType(typeof(GetContacts.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetContacts.Response>> Get()
        => await _meditator.Send(new GetContacts.Request());
    [HttpGet("{contactId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetContactById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetContactById.Response>> GetById([FromRoute] GetContactById.Request request)
        => await _meditator.Send(request);
}
