// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.Roles;

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(x => x.RoleId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class RoleDto
{
    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class RoleExtensions
{
    public static RoleDto ToDto(this Role role)
        => new RoleDto
        {
            RoleId = role.RoleId,
            Name = role.Name,
            Version = role.Version
        };
}

