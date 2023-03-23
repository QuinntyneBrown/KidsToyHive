// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.HtmlContents;

public class GetHtmlContentByIdRequest : IRequest<GetHtmlContentByIdResponse>
{
    public Guid HtmlContentId { get; set; }
}
public class GetHtmlContentByIdResponse
{
    public HtmlContentDto HtmlContent { get; set; }
}
public class GetHtmlContentByIdHandler : IRequestHandler<GetHtmlContentByIdRequest, GetHtmlContentByIdResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetHtmlContentByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetHtmlContentByIdResponse> Handle(GetHtmlContentByIdRequest request, CancellationToken cancellationToken)
        => new GetHtmlContentByIdResponse()
        {
            HtmlContent = (await _context.HtmlContents.FindAsync(request.HtmlContentId)).ToDto()
        };
}

