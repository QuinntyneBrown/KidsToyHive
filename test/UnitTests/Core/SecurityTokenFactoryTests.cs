// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace UnitTests.Core;

public class SecurityTokenFactoryTests
{
    private readonly SecurityTokenFactory _securityTokenFactory = new SecurityTokenFactory(ConfigurationHelper.OAuth2);
    private readonly TokenValidationParameters _tokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationHelper.OAuth2["Authentication:JwtKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    [Fact]
    public void ShouldCreateValidToken()
    {
        var token = _securityTokenFactory.Create("Username");
        var principal = new JwtSecurityTokenHandler().ValidateToken(token, _tokenValidationParameters, out _);
        Assert.NotNull(principal);
        Assert.Equal("Username", principal.Identity.Name);
        Assert.Contains(principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value, "Username");
    }
}

