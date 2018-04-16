using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Features.Accounts
{
    public class GetAccountsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<AccountApiModel> Accounts { get; set; }
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
                    Accounts = await _context.Accounts.Select(x => AccountApiModel.FromAccount(x)).ToListAsync()
                };
        }
    }
}
