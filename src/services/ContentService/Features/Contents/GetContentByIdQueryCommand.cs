using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ContentService.Features.Contents
{
    public class GetContentByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContentId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ContentId { get; set; }
        }

        public class Response
        {
            public ContentApiModel Content { get; set; }
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
                    Content = ContentApiModel.FromContent(await _context.Contents.FindAsync(request.ContentId))
                };
        }
    }
}
