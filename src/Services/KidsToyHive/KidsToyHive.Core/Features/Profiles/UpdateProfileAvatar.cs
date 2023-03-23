// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Profiles;

public class UpdateProfileAvatarRequest : IRequest<UpdateProfileAvatarResponse>
{
    public int ProfileId { get; set; }
    public string AvatarUrl { get; set; }
}
public class UpdateProfileAvatarResponse
{
    public Guid ProfileId { get; set; }
}
public class UpdateProfileAvatarHandler : IRequestHandler<UpdateProfileAvatarRequest, UpdateProfileAvatarResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpdateProfileAvatarHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpdateProfileAvatarResponse> Handle(UpdateProfileAvatarRequest request, CancellationToken cancellationToken)
    {
        var profile = _context.Profiles.Find(request.ProfileId);
        profile.AvatarUrl = request.AvatarUrl;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateProfileAvatarResponse()
        {
            ProfileId = profile.ProfileId
        };
    }
}

