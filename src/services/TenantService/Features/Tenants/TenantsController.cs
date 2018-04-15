using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TenantService.Features.Tenants
{
    [ApiController]
    [Route("api/tenants")]
    public class TenantsController
    {
        private readonly IMediator _mediator;

        public TenantsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveTenantCommand.Response>> Save(SaveTenantCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Tenant.TenantId}")]
        public async Task Remove(RemoveTenantCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{TenantId}")]
        public async Task<ActionResult<GetTenantByIdQuery.Response>> GetById([FromRoute]GetTenantByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetTenantsQuery.Response>> Get()
            => await _mediator.Send(new GetTenantsQuery.Request());
    }
}
