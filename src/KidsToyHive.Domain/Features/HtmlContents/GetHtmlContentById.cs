using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.HtmlContents;

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
     private readonly IAppDbContext _context;
     public GetHtmlContentByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetHtmlContentByIdResponse> Handle(GetHtmlContentByIdRequest request, CancellationToken cancellationToken)
         => new GetHtmlContentByIdResponse()
         {
             HtmlContent = (await _context.HtmlContents.FindAsync(request.HtmlContentId)).ToDto()
         };
 }
