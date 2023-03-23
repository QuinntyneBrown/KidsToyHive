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
    [ProducesResponseType(typeof(GetTaxesResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetTaxesResponse>> Get()
        => await _meditator.Send(new GetTaxesRequest());
    [HttpGet("{taxId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetTaxByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetTaxByIdResponse>> GetById([FromRoute] GetTaxByIdRequest request)
        => await _meditator.Send(request);
}
