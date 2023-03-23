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
    [ProducesResponseType(typeof(GetHtmlContentsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetHtmlContentsResponse>> Get()
        => await _meditator.Send(new GetHtmlContentsRequest());
    [AllowAnonymous]
    [HttpGet("{htmlContentId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetHtmlContentByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetHtmlContentByIdResponse>> GetById([FromRoute] GetHtmlContentByIdRequest request)
        => await _meditator.Send(request);
    [AllowAnonymous]
    [HttpGet("name/{name}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetHtmlContentByNameResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetHtmlContentByNameResponse>> GetByName([FromRoute] GetHtmlContentByNameRequest request)
        => await _meditator.Send(request);
}
