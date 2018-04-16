using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace AccountService.Features.Accounts
{
    public class GetAccountByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.AccountId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int AccountId { get; set; }
        }

        public class Response
        {
            public AccountApiModel Account { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Account = AccountApiModel.FromAccount(await _context.Accounts.FindAsync(request.AccountId))
                };
        }
    }
}
