using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.HtmlContents;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.HtmlContentId).NotNull();
    }
}
public class RemoveHtmlContentRequest : IRequest
{
    public Guid HtmlContentId { get; set; }
}
public class RemoveHtmlContentHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveHtmlContentHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveHtmlContentRequest request, CancellationToken cancellationToken)
    {
        var htmlContent = await _context.HtmlContents.FindAsync(request.HtmlContentId);
        _context.HtmlContents.Remove(htmlContent);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
