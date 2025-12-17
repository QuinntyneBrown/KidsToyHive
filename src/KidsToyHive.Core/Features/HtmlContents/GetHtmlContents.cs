// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.HtmlContents;

public class GetHtmlContentsRequest : IRequest<GetHtmlContentsResponse> { }
public class GetHtmlContentsResponse
{
    public IEnumerable<HtmlContentDto> HtmlContents { get; set; }
}
public class GetHtmlContentsHandler : IRequestHandler<GetHtmlContentsRequest, GetHtmlContentsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetHtmlContentsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetHtmlContentsResponse> Handle(GetHtmlContentsRequest request, CancellationToken cancellationToken)
        => new GetHtmlContentsResponse()
        {
            HtmlContents = await _context.HtmlContents.Select(x => x.ToDto()).ToArrayAsync()
        };
}

