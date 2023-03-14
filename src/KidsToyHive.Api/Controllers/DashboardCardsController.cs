using KidsToyHive.Domain.Features.DashboardCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/dashboardCards")]
public class DashboardCardsController
{
    private readonly IMediator _meditator;
    public DashboardCardsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDashboardCards.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDashboardCards.Response>> Get()
        => await _meditator.Send(new GetDashboardCards.Request());
    [HttpGet("{dashboardCardId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetDashboardCardById.Response), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetDashboardCardById.Response>> GetById([FromRoute] GetDashboardCardById.Request request)
        => await _meditator.Send(request);
}
