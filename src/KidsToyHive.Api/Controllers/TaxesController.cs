using KidsToyHive.Domain.Features.Taxes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/taxes")]
public class TaxesController
{
    private readonly IMediator _meditator;
    public TaxesController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetTaxes.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetTaxes.Response>> Get()
        => await _meditator.Send(new GetTaxes.Request());
    [HttpGet("{taxId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetTaxById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetTaxById.Response>> GetById([FromRoute] GetTaxById.Request request)
        => await _meditator.Send(request);
}
