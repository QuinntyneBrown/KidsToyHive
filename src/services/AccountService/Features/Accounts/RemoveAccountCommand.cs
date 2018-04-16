using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace AccountService.Features.Accounts
{
    public class RemoveAccountCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Account.AccountId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Account Account { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Accounts.Remove(await _context.Accounts.FindAsync(request.Account.AccountId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
