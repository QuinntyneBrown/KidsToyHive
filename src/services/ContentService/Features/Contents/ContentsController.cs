using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContentService.Features.Contents
{
    [ApiController]
    [Route("api/contents")]
    public class ContentsController
    {
        private readonly IMediator _mediator;

        public ContentsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveContentCommand.Response>> Save(SaveContentCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Content.ContentId}")]
        public async Task Remove(RemoveContentCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{ContentId}")]
        public async Task<ActionResult<GetContentByIdQuery.Response>> GetById([FromRoute]GetContentByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetContentsQuery.Response>> Get()
            => await _mediator.Send(new GetContentsQuery.Request());
    }
}
