using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ContentService.Features.Contents
{
    public class RemoveContentCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Content.ContentId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Content Content { get; set; }
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
                _context.Contents.Remove(await _context.Contents.FindAsync(request.Content.ContentId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
