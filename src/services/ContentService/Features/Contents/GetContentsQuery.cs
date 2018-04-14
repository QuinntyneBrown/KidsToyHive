using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Features.Contents
{
    public class GetContentsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ContentApiModel> Contents { get; set; }
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
                    Contents = await _context.Contents.Select(x => ContentApiModel.FromContent(x)).ToListAsync()
                };
        }
    }
}
