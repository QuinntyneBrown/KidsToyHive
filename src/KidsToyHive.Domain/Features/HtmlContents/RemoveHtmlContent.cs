using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.HtmlContents;

public class RemoveHtmlContent
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.HtmlContentId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid HtmlContentId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var htmlContent = await _context.HtmlContents.FindAsync(request.HtmlContentId);
            _context.HtmlContents.Remove(htmlContent);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
