using KidsToyHive.Domain.Features.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController
{
    private readonly IMediator _meditator;
    public RolesController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetRolesResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetRolesResponse>> Get()
        => await _meditator.Send(new GetRolesRequest());
    [HttpGet("{roleId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetRoleByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetRoleByIdResponse>> GetById([FromRoute] GetRoleByIdRequest request)
        => await _meditator.Send(request);
}
