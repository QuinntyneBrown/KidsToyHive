using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactService.Features.Contacts
{
    [Authorize]
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveContactCommand.Response>> Save(SaveContactCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Contact.ContactId}")]
        public async Task Remove(RemoveContactCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ContactId}")]
        public async Task<ActionResult<GetContactByIdQuery.Response>> GetById([FromRoute]GetContactByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetContactsQuery.Response>> Get()
            => await _mediator.Send(new GetContactsQuery.Request());
    }
}
