using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Videos;

public class GetVideoByIdRequest : IRequest<GetVideoByIdResponse>
{
    public Guid VideoId { get; set; }
}
public class GetVideoByIdResponse
{
    public VideoDto Video { get; set; }
}
public class GetVideoByIdHandler : IRequestHandler<GetVideoByIdRequest, GetVideoByIdResponse>
{
    public IAppDbContext _context { get; set; }
    public GetVideoByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetVideoByIdResponse> Handle(GetVideoByIdRequest request, CancellationToken cancellationToken)
        => new GetVideoByIdResponse()
        {
            Video = (await _context.Videos.FindAsync(request.VideoId)).ToDto()
        };
}
