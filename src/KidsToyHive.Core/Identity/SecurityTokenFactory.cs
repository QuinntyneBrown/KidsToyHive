using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KidsToyHive.Core.Identity
{

    public class SecurityTokenFactory : ISecurityTokenFactory
    {
        private IConfiguration _configuration;

        public SecurityTokenFactory(IConfiguration configuration)
            => _configuration = configuration;

        public string Create(string username, List<Claim> customClaims = null)
        {
            var now = DateTime.UtcNow;
            var nowDateTimeOffset = new DateTimeOffset(now);

            var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, username),
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, nowDateTimeOffset.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                };

            if (customClaims != null)
                claims.AddRange(customClaims);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtIssuer"],
                audience: _configuration["Authentication:JwtAudience"],
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(Convert.ToInt16(_configuration["Authentication:ExpirationMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:JwtKey"])), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
