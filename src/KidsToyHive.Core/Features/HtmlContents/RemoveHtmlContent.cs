// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.HtmlContents;

public class RemoveHtmlContentValidator : AbstractValidator<RemoveHtmlContentRequest>
{
    public RemoveHtmlContentValidator()
    {
        RuleFor(request => request.HtmlContentId).NotNull();
    }
}
public class RemoveHtmlContentRequest : IRequest
{
    public Guid HtmlContentId { get; set; }
}
public class RemoveHtmlContentHandler : IRequestHandler<RemoveHtmlContentRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveHtmlContentHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveHtmlContentRequest request, CancellationToken cancellationToken)
    {
        var htmlContent = await _context.HtmlContents.FindAsync(request.HtmlContentId);
        _context.HtmlContents.Remove(htmlContent);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

