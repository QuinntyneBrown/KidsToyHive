using KidsToyHive.Domain.Features.Bins;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/bins")]
public class BinsController
{
    private readonly IMediator _meditator;
    public BinsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBins.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBins.Response>> Get()
        => await _meditator.Send(new GetBins.Request());
    [HttpGet("{binId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBinById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBinById.Response>> GetById([FromRoute] GetBinById.Request request)
        => await _meditator.Send(request);
}
