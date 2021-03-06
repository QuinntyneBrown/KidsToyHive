using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.HtmlContents
{
    public class GetHtmlContentById
    {
        public class Request : IRequest<Response> {
            public Guid HtmlContentId { get; set; }
        }

        public class Response
        {
            public HtmlContentDto HtmlContent { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    HtmlContent = (await _context.HtmlContents.FindAsync(request.HtmlContentId)).ToDto()
                };
        }
    }
}
