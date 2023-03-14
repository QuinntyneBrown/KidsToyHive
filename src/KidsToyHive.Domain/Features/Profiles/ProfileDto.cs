using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Models;
using System;

namespace KidsToyHive.Domain.Features.Profiles;

public class ProfileDto
{
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public ProfileType Type { get; set; }
    public string AvatarUrl { get; set; }
    public static ProfileDto FromProfile(Profile profile)
        => new ProfileDto
        {
            ProfileId = profile.ProfileId,
            Name = profile.Name,
            AvatarUrl = profile.AvatarUrl,
            Type = profile.Type
        };
}
