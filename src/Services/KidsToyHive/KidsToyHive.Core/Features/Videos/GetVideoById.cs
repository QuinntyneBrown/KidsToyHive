// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Videos;

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
    public IKidsToyHiveDbContext _context { get; set; }
    public GetVideoByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetVideoByIdResponse> Handle(GetVideoByIdRequest request, CancellationToken cancellationToken)
        => new GetVideoByIdResponse()
        {
            Video = (await _context.Videos.FindAsync(request.VideoId)).ToDto()
        };
}

