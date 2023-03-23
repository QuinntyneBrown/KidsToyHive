// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Profiles;

public class UpsertProfileValidator : AbstractValidator<UpsertProfileRequest>
{
    public UpsertProfileValidator()
    {
        RuleFor(request => request.Profile.ProfileId).NotNull();
    }
}
public class UpsertProfileRequest : IRequest<UpsertProfileResponse>
{
    public ProfileDto Profile { get; set; }
}
public class UpsertProfileResponse
{
    public Guid ProfileId { get; set; }
}
public class UpsertProfileHandler : IRequestHandler<UpsertProfileRequest, UpsertProfileResponse>
{
    private readonly IKidsToyHiveDbContext _context;

    public UpsertProfileHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertProfileResponse> Handle(UpsertProfileRequest request, CancellationToken cancellationToken)
    {
        var profile = await _context.Profiles.FindAsync(request.Profile.ProfileId);
        if (profile == null) _context.Profiles.Add(profile = new Profile());
        profile.Name = request.Profile.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertProfileResponse() { ProfileId = profile.ProfileId };
    }
}

