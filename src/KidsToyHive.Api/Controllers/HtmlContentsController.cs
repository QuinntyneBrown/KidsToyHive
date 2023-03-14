using KidsToyHive.Domain.Features.HtmlContents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/htmlContents")]
public class HtmlContentsController
{
    private readonly IMediator _meditator;
    public HtmlContentsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetHtmlContents.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetHtmlContents.Response>> Get()
        => await _meditator.Send(new GetHtmlContents.Request());
    [AllowAnonymous]
    [HttpGet("{htmlContentId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetHtmlContentById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetHtmlContentById.Response>> GetById([FromRoute] GetHtmlContentById.Request request)
        => await _meditator.Send(request);
    [AllowAnonymous]
    [HttpGet("name/{name}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetHtmlContentByName.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetHtmlContentByName.Response>> GetByName([FromRoute] GetHtmlContentByName.Request request)
        => await _meditator.Send(request);
}
