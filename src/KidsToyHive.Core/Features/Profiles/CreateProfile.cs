// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Identity;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Profiles;

public class CreateProfileRequest : IRequest<CreateProfileResponse>
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
public class CreateProfileResponse
{
    public Guid ProfileId { get; set; }
}
public class CreateProfileHandler : IRequestHandler<CreateProfileRequest, CreateProfileResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public IPasswordHasher _passwordHasher { get; set; }
    public CreateProfileHandler(IKidsToyHiveDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<CreateProfileResponse> Handle(CreateProfileRequest request, CancellationToken cancellationToken)
    {
        var profile = new Profile() { Name = request.Name, AvatarUrl = request.AvatarUrl };
        profile.User = new User() { Username = request.Username };
        profile.User.Password = _passwordHasher.HashPassword(profile.User.Salt, request.Password);
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateProfileResponse() { ProfileId = profile.ProfileId };
    }
}

