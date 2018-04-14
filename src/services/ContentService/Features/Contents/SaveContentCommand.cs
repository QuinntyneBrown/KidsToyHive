using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ContentService.Features.Contents
{
    public class SaveContentCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Content.ContentId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ContentApiModel Content { get; set; }
        }

        public class Response
        {			
            public int ContentId { get; set; }
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
                var content = await _context.Contents.FindAsync(request.Content.ContentId);

                if (content == null) _context.Contents.Add(content = new Content());

                content.Name = request.Content.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ContentId = content.ContentId };
            }
        }
    }
}
