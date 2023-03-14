using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Domain.Features.HtmlContents;

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
    private readonly IAppDbContext _context;
    public GetHtmlContentByNameHandler(IAppDbContext context) => _context = context;
    public async Task<GetHtmlContentByNameResponse> Handle(GetHtmlContentByNameRequest request, CancellationToken cancellationToken)
        => new GetHtmlContentByNameResponse()
        {
            HtmlContent = (await _context.HtmlContents.SingleAsync(x => x.Name == request.Name)).ToDto()
        };
}
