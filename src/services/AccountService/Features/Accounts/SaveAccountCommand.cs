using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace AccountService.Features.Accounts
{
    public class SaveAccountCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Account.AccountId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public AccountApiModel Account { get; set; }
        }

        public class Response
        {			
            public int AccountId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var account = await _context.Accounts.FindAsync(request.Account.AccountId);

                if (account == null) _context.Accounts.Add(account = new Account());

                account.Name = request.Account.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { AccountId = account.AccountId };
            }
        }
    }
}
