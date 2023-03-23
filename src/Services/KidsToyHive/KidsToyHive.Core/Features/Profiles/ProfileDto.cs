// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Models;
using System;

namespace KidsToyHive.Core.Features.Profiles;

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

