using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.HtmlContents;

public class UpsertHtmlContentValidator : AbstractValidator<UpsertHtmlContentRequest>
{
    public UpsertHtmlContentValidator()
    {
        RuleFor(request => request.HtmlContent).NotNull();
        RuleFor(request => request.HtmlContent).SetValidator(new HtmlContentDtoValidator());
    }
}
public class UpsertHtmlContentRequest : IRequest<UpsertHtmlContentResponse>
{
    public HtmlContentDto HtmlContent { get; set; }
}
public class UpsertHtmlContentResponse
{
    public Guid HtmlContentId { get; set; }
}
public class UpsertHtmlContentHandler : IRequestHandler<UpsertHtmlContentRequest, UpsertHtmlContentResponse>
{
    private readonly IAppDbContext _context;
    public UpsertHtmlContentHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertHtmlContentResponse> Handle(UpsertHtmlContentRequest request, CancellationToken cancellationToken)
    {
        var htmlContent = await _context.HtmlContents.FindAsync(request.HtmlContent.HtmlContentId);
        if (htmlContent == null)
        {
            htmlContent = new HtmlContent();
            _context.HtmlContents.Add(htmlContent);
        }
        htmlContent.Name = request.HtmlContent.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertHtmlContentResponse() { HtmlContentId = htmlContent.HtmlContentId };
    }
}
