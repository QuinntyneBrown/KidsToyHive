// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Exceptions;
using KidsToyHive.Core.Identity;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Users;

public class AuthenticateValidator : AbstractValidator<AuthenticateRequest>
{
    public AuthenticateValidator()
    {
        RuleFor(request => request.Username)
            .NotEqual(default(string))
            .EmailAddress();
        RuleFor(request => request.Password).NotEqual(default(string));
    }
}
public class AuthenticateRequest : IRequest<AuthenticateResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class AuthenticateResponse
{
    public string AccessToken { get; set; }
    public Guid UserId { get; set; }
}
public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISecurityTokenFactory _securityTokenFactory;
    public AuthenticateHandler(IKidsToyHiveDbContext context, ISecurityTokenFactory securityTokenFactory, IPasswordHasher passwordHasher)
    {
        _context = context;
        _securityTokenFactory = securityTokenFactory;
        _passwordHasher = passwordHasher;
    }
    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        User user = await _context.Users
            .SingleOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());
        if (user == null)
            throw new InvalidUsernameOrPasswordException();
        if (!ValidateUser(user, _passwordHasher.HashPassword(user.Salt, request.Password)))
            throw new InvalidUsernameOrPasswordException();
        var profiles = _context.Profiles
            .Include(x => x.User)
            .Where(x => x.User.Username == request.Username)
            .ToList();
        var claims = new List<Claim>
          {
              new Claim("UserId", $"{user.UserId}"),
              new Claim("PartitionKey", $"{user.TenantId}"),
              new Claim("CurrentUserName", $"{user.Username}")
          };
        foreach (var profile in profiles)
        {
            claims.Add(new Claim("ProfileId", $"{profile.ProfileId}"));
            if (profile.Type == ProfileType.Customer)
                claims.Add(new Claim(ClaimTypes.Role, nameof(ProfileType.Customer)));
            if (profile.Type == ProfileType.Admin)
                claims.Add(new Claim(ClaimTypes.Role, nameof(ProfileType.Admin)));
            if (profile.Type == ProfileType.Driver)
                claims.Add(new Claim(ClaimTypes.Role, nameof(ProfileType.Driver)));
        }
        return new AuthenticateResponse()
        {
            AccessToken = _securityTokenFactory.Create(request.Username, claims),
            UserId = user.UserId
        };
    }
    public bool ValidateUser(User user, string transformedPassword)
    {
        if (user == null || transformedPassword == null)
            return false;
        return user.Password == transformedPassword;
    }
}

