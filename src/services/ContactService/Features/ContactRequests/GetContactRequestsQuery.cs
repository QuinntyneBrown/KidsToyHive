using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Features.ContactRequests
{
    public class GetContactRequestsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ContactRequestApiModel> ContactRequests { get; set; }
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
                    ContactRequests = await _context.ContactRequests.Select(x => ContactRequestApiModel.FromContactRequest(x)).ToListAsync()
                };
        }
    }
}
