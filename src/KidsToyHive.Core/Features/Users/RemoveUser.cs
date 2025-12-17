// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Users;

public class RemoveUserValidator : AbstractValidator<RemoveUserRequest>
{
    public RemoveUserValidator()
    {
        RuleFor(request => request.UserId).NotNull();
    }
}
public class RemoveUserRequest : IRequest
{
    public Guid UserId { get; set; }
}
public class RemoveUserHandler : IRequestHandler<RemoveUserRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveUserHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

