// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Roles;

public class RemoveRoleValidator : AbstractValidator<RemoveRoleRequest>
{
    public RemoveRoleValidator()
    {
        RuleFor(request => request.RoleId).NotNull();
    }
}
public class RemoveRoleRequest : IRequest
{
    public Guid RoleId { get; set; }
}
public class RemoveRoleHandler : IRequestHandler<RemoveRoleRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveRoleHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FindAsync(request.RoleId);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

