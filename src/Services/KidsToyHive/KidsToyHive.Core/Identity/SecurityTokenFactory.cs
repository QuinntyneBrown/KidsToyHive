// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KidsToyHive.Core.Identity;

// Builder pattern? https://github.com/QuinntyneBrown/grandnode/blob/develop/Grand.Api/Jwt/JwtTokenBuilder.cs
public class SecurityTokenFactory : ISecurityTokenFactory
{
    private IConfiguration _configuration;
    public SecurityTokenFactory(IConfiguration configuration)
        => _configuration = configuration;
    public string Create(string username, List<Claim> additionalClaims = default)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
        if (additionalClaims != default) claims.AddRange(additionalClaims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Authentication:ExpirationMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:JwtKey"])),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

