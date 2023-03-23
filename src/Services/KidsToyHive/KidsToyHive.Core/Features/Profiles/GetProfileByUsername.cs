// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using KidsToyHive.Core;

namespace KidsToyHive.Core.Features.Profiles;

public class GetProfileByUsernameRequest : IRequest<GetProfileByUsernameResponse>
{
    public string Username { get; set; }
}
public class GetProfileByUsernameResponse
{
    public ProfileDto Profile { get; set; }
}
public class GetProfileByUsernameHandler : IRequestHandler<GetProfileByUsernameRequest, GetProfileByUsernameResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetProfileByUsernameHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetProfileByUsernameResponse> Handle(GetProfileByUsernameRequest request, CancellationToken cancellationToken)
        => new GetProfileByUsernameResponse()
        {
            Profile = ProfileDto.FromProfile(await _context.Profiles.SingleAsync(x => x.User.Username == request.Username))
        };
}

