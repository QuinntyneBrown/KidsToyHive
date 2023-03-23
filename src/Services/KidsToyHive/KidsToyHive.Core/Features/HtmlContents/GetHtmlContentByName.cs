// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Core.Features.HtmlContents;

public class GetHtmlContentByNameRequest : IRequest<GetHtmlContentByNameResponse>
{
    public string Name { get; set; }
}
public class GetHtmlContentByNameResponse
{
    public HtmlContentDto HtmlContent { get; set; }
}
public class GetHtmlContentByNameHandler : IRequestHandler<GetHtmlContentByNameRequest, GetHtmlContentByNameResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetHtmlContentByNameHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetHtmlContentByNameResponse> Handle(GetHtmlContentByNameRequest request, CancellationToken cancellationToken)
        => new GetHtmlContentByNameResponse()
        {
            HtmlContent = (await _context.HtmlContents.SingleAsync(x => x.Name == request.Name)).ToDto()
        };
}

