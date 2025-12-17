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

namespace KidsToyHive.Core.Features.Videos;

public class GetVideosRequest : IRequest<GetVideosResponse> { }
public class GetVideosResponse
{
    public IEnumerable<VideoDto> Videos { get; set; }
}
public class GetVideosHandler : IRequestHandler<GetVideosRequest, GetVideosResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetVideosHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetVideosResponse> Handle(GetVideosRequest request, CancellationToken cancellationToken)
        => new GetVideosResponse()
        {
            Videos = await _context.Videos.Select(x => x.ToDto()).ToArrayAsync()
        };
}

