using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KidsToyHive.Core.Identity
{
    //https://github.com/bradygaster/DotNetCore3WithJwtAuth/blob/master/User.cs


    public class SecurityTokenFactory : ISecurityTokenFactory
    {
        private IConfiguration _configuration;

        public SecurityTokenFactory(IConfiguration configuration)
            => _configuration = configuration;

        public string Create(string username, List<Claim> additionalClaims = default)
        {            
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username), new Claim(JwtRegisteredClaimNames.UniqueName, username) };

            if(additionalClaims != default) claims.AddRange(additionalClaims);

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
}
