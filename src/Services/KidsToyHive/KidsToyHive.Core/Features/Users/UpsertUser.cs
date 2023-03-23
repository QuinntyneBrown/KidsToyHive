// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Users;

public class UpsertUserValidator : AbstractValidator<UpsertUserRequest>
{
    public UpsertUserValidator()
    {
        RuleFor(request => request.User).NotNull();
        RuleFor(request => request.User).SetValidator(new UserDtoValidator());
    }
}
public class UpsertUserRequest : IRequest<UpsertUserResponse>
{
    public UserDto User { get; set; }
}
public class UpsertUserResponse
{
    public Guid UserId { get; set; }
}
public class UpsertUserHandler : IRequestHandler<UpsertUserRequest, UpsertUserResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertUserHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertUserResponse> Handle(UpsertUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.User.UserId);
        if (user == null)
        {
            user = new User();
            _context.Users.Add(user);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertUserResponse() { UserId = user.UserId };
    }
}

