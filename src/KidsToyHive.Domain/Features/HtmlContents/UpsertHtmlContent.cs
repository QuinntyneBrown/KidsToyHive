using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.HtmlContents;

public class UpsertHtmlContent
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.HtmlContent).NotNull();
            RuleFor(request => request.HtmlContent).SetValidator(new HtmlContentDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public HtmlContentDto HtmlContent { get; set; }
    }
    public class Response
    {
        public Guid HtmlContentId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var htmlContent = await _context.HtmlContents.FindAsync(request.HtmlContent.HtmlContentId);
            if (htmlContent == null)
            {
                htmlContent = new HtmlContent();
                _context.HtmlContents.Add(htmlContent);
            }
            htmlContent.Name = request.HtmlContent.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { HtmlContentId = htmlContent.HtmlContentId };
        }
    }
}
