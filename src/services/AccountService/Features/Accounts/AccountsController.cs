using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountService.Features.Accounts
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveAccountCommand.Response>> Save(SaveAccountCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{Account.AccountId}")]
        public async Task Remove(RemoveAccountCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{AccountId}")]
        public async Task<ActionResult<GetAccountByIdQuery.Response>> GetById([FromRoute]GetAccountByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetAccountsQuery.Response>> Get()
            => await _mediator.Send(new GetAccountsQuery.Request());
    }
}
