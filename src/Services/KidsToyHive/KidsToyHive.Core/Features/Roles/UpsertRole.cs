// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Roles;

public class UpsertRoleValidator : AbstractValidator<UpsertRoleRequest>
{
    public UpsertRoleValidator()
    {
        RuleFor(request => request.Role).NotNull();
        RuleFor(request => request.Role).SetValidator(new RoleDtoValidator());
    }
}
public class UpsertRoleRequest : IRequest<UpsertRoleResponse>
{
    public RoleDto Role { get; set; }
}
public class UpsertRoleResponse
{
    public Guid RoleId { get; set; }
}
public class UpsertRoleHandler : IRequestHandler<UpsertRoleRequest, UpsertRoleResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertRoleHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertRoleResponse> Handle(UpsertRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FindAsync(request.Role.RoleId);
        if (role == null)
        {
            role = new Role();
            _context.Roles.Add(role);
        }
        role.Name = request.Role.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertRoleResponse() { RoleId = role.RoleId };
    }
}

