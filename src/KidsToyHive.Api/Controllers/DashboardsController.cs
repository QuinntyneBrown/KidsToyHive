using KidsToyHive.Domain.Features.Dashboards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/dashboards")]
    public class DashboardsController
    {
        private readonly IMediator _meditator;

        public DashboardsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDashboards.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDashboards.Response>> Get()
            => await _meditator.Send(new GetDashboards.Request());

        [HttpGet("{dashboardId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetDashboardById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetDashboardById.Response>> GetById([FromRoute]GetDashboardById.Request request)
            => await _meditator.Send(request);
    }
}
