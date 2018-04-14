using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactService.Features.ContactRequests
{
    [Authorize]
    [ApiController]
    [Route("api/contactRequests")]
    public class ContactRequestsController
    {
        private readonly IMediator _mediator;

        public ContactRequestsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveContactRequestCommand.Response>> Save(SaveContactRequestCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{ContactRequest.ContactRequestId}")]
        public async Task Remove(RemoveContactRequestCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ContactRequestId}")]
        public async Task<ActionResult<GetContactRequestByIdQuery.Response>> GetById([FromRoute]GetContactRequestByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetContactRequestsQuery.Response>> Get()
            => await _mediator.Send(new GetContactRequestsQuery.Request());
    }
}
