using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Users;

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
