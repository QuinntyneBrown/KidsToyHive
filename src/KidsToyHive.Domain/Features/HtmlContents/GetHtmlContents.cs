using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.HtmlContents;

public class GetHtmlContentsRequest : IRequest<GetHtmlContentsResponse> { }
public class GetHtmlContentsResponse
{
    public IEnumerable<HtmlContentDto> HtmlContents { get; set; }
}
public class GetHtmlContentsHandler : IRequestHandler<GetHtmlContentsRequest, GetHtmlContentsResponse>
{
    private readonly IAppDbContext _context;
    public GetHtmlContentsHandler(IAppDbContext context) => _context = context;
    public async Task<GetHtmlContentsResponse> Handle(GetHtmlContentsRequest request, CancellationToken cancellationToken)
        => new GetHtmlContentsResponse()
        {
            HtmlContents = await _context.HtmlContents.Select(x => x.ToDto()).ToArrayAsync()
        };
}
