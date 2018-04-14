using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatService.Features.Messages
{
    [Authorize]
    [ApiController]
    [Route("api/messages")]
    public class MessagesController
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveMessageCommand.Response>> Save(SaveMessageCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Message.MessageId}")]
        public async Task Remove(RemoveMessageCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{MessageId}")]
        public async Task<ActionResult<GetMessageByIdQuery.Response>> GetById([FromRoute]GetMessageByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetMessagesQuery.Response>> Get()
            => await _mediator.Send(new GetMessagesQuery.Request());
    }
}
