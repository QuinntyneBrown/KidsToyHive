// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using KidsToyHive.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KidsToyHive.Core;

namespace KidsToyHive.Core.Features.Profiles;

public class GetProfilesRequest : IRequest<GetProfilesResponse> { }
public class GetProfilesResponse
{
    public IEnumerable<ProfileDto> Profiles { get; set; }
}
public class GetProfilesHandler : IRequestHandler<GetProfilesRequest, GetProfilesResponse>
{
    private readonly IKidsToyHiveDbContext _context;

    public GetProfilesHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProfilesResponse> Handle(GetProfilesRequest request, CancellationToken cancellationToken)
        => new GetProfilesResponse()
        {
            Profiles = await _context.Profiles.Select(x => ProfileDto.FromProfile(x)).ToListAsync()
        };
}

