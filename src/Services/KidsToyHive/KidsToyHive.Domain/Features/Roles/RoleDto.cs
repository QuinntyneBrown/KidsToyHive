using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Roles;

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
