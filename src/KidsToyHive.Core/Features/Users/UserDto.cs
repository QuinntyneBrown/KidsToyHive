// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.Users;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
    }
}
public class UserDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class UserExtensions
{
    public static UserDto ToDto(this User user)
        => new UserDto
        {
            UserId = user.UserId,
            Version = user.Version
        };
}

