// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Core.Models;

public class Profile
{
    public Guid ProfileId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public string AvatarUrl { get; set; }
    public ProfileType Type { get; set; }
    public int Version { get; set; }
    public User User { get; set; }
}

